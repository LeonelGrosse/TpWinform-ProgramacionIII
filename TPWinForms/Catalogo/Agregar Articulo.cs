﻿using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace Catalogo
{
    public partial class frmAgregarArticulo : Form
    {
        private articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }
        public frmAgregarArticulo(articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            marcaNegocio marca = new marcaNegocio();    
            categoriaNegocio categoria = new categoriaNegocio();
            try
            {
                cbxMarca.DataSource = marca.listar();
                cbxMarca.ValueMember = "idMarca";
                cbxMarca.DisplayMember = "nombre";
                cbxCategoria.DataSource = categoria.listar();
                cbxCategoria.ValueMember = "idCategoria";
                cbxCategoria.DisplayMember = "nombre";
                
                if(articulo != null)
                {
                    txtCodigo.Text = articulo.codigo;
                    txtNombre.Text = articulo.nombre;
                    txtDescripcion.Text = articulo.descripcion;
                    txtPrecio.Text = articulo.precio.ToString();
                    cbxMarca.SelectedValue = articulo.marca.idMarca;
                    cbxCategoria.SelectedValue = articulo.categoria.idCategoria;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            articuloNegocio negocio = new articuloNegocio();

            try
            {
                if(articulo == null)
                    articulo = new articulo(); 
                articulo.codigo = txtCodigo.Text;
                articulo.nombre = txtNombre.Text;    
                articulo.descripcion = txtDescripcion.Text;
                articulo.marca = (marca)cbxMarca.SelectedItem;
                articulo.categoria =(categoria)cbxCategoria.SelectedItem;
                articulo.precio = int.Parse(txtPrecio.Text);                     //Aca se rompe si se quiere agregar o modificar un articulo y no se ingresa un int. La tabla de sql es dato decimal pero no se como hacer para que tome numero float

                if (articulo.idArticulo != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
