﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proiect2.Entity;
using Proiect2.Service;

namespace Proiect2
{
    public partial class AdauagreForm : Form
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _categoryService;

        public AdauagreForm(ProductService productService, ProductCategoryService categoryService)
        {
            InitializeComponent();
            _productService = productService;
            _categoryService = categoryService;

            LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllProductCategories();
            comboBox1.DataSource = categories;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Name = textBox1.Text,
                Description = textBox2.Text,
                EntryDate = dateTimePicker1.Value,
                ExpiryDate = dateTimePicker2.Value,
                Quantity = (int)numericUpDown1.Value,
                CategoryId = (int)comboBox1.SelectedValue
            };

            if (product.Quantity <= 0 || string.IsNullOrWhiteSpace(product.Description) || string.IsNullOrWhiteSpace(product.Name))
            {
                MessageBox.Show("Date insuficiente", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                await _productService.AddProduct(product);
                MessageBox.Show("Produsul a fost adaugat cu succes!", "Confirmare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

}