﻿using System;
using System.Windows.Forms;
using Proiect2.Data;
using Proiect2.Repository;
using Proiect2.Service;

namespace Proiect2
{
    public partial class LoginForm : Form
    {
        private readonly ProductService _productService;
        private readonly ProductCategoryService _productCategoryService;
        private readonly SalesHistoryService _salesHistoryService;
        private readonly UserService _userService;

        public LoginForm()
        {
            InitializeComponent();

            var context = DbContextFactory.CreateDbContext();

            var productRepository = new ProductRepository(context);
            _productService = new ProductService(productRepository);

            var productCategoryRepository = new ProductCategoryRepository(context);
            _productCategoryService = new ProductCategoryService(productCategoryRepository);

            var salesHistoryRepository = new SalesHistoryRepository(context);
            _salesHistoryService = new SalesHistoryService(salesHistoryRepository);

            var userRepository = new UserRepository(context);
            _userService = new UserService(userRepository);
        }

        private async void loginBtn_Click(object sender, EventArgs e)
        {
            string username = userTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            try
            {
                await _userService.AuthenticateUser(username, password);

                using (var form = new Form1(_productService, _salesHistoryService, _productCategoryService, _userService))
                {
                    this.Hide();
                    form.ShowDialog();
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Authentication failed: " + ex.Message);
            }
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            using (var form = new RegisterForm(_userService))
            {
                form.ShowDialog();
            }
        }
    }
}