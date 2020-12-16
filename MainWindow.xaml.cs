using MotoLotModel;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Customer = MotoLotModel.Customer;
using Order = MotoLotModel.Order;

namespace Serb_Cristiana_Maria_Project
{
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        //using AutoLotModel;
        ActionState action = ActionState.Nothing;
        MotoLotEntitiesModel ctx = new MotoLotEntitiesModel();
        CollectionViewSource customerViewSource;
        CollectionViewSource catalogOrdersViewSource;
        CollectionViewSource catalogViewSource;
        Binding custIdTextBoxBinding = new Binding();
        Binding firstNameTextBoxBinding = new Binding();
        Binding lastNameTextBoxBinding = new Binding();
        Binding countryTextBoxBinding = new Binding();
        Binding txtCatalogBinding = new Binding();
        Binding txtCustomerBinding = new Binding();
        Binding motorcycleIdTextBoxBinding = new Binding();
        Binding colorTextBoxBinding = new Binding();
        Binding companyTextBoxBinding = new Binding();
        Binding priceEURTextBoxBinding = new Binding();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerViewSource.Source = ctx.Customers.Local;
            catalogOrdersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("catalogOrdersViewSource")));
            catalogOrdersViewSource.Source = ctx.Orders.Local;
            catalogViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("catalogViewSource")));
            catalogViewSource.Source = ctx.Catalogs.Local;
            ctx.Customers.Load();
            ctx.Orders.Load();
            ctx.Catalogs.Load();
            cmbCustomers.ItemsSource = ctx.Customers.Local;
            cmbCatalog.ItemsSource = ctx.Catalogs.Local;
            BindDataGrid();
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            custIdTextBox.IsEnabled = true;
            firstNameTextBox.IsEnabled = true;
            lastNameTextBox.IsEnabled = true;
            countryTextBox.IsEnabled = true;
            BindingOperations.ClearBinding(custIdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(countryTextBox, TextBox.TextProperty);
            custIdTextBox.Text = "";
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            countryTextBox.Text = "";
            Keyboard.Focus(custIdTextBox);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempcustIdTextBox = custIdTextBox.Text.ToString();
            string tempfirstNameTextBox = firstNameTextBox.Text.ToString();
            string templastNameTextBox = lastNameTextBox.Text.ToString();
            string tempcountryTextBox = countryTextBox.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            custIdTextBox.IsEnabled = true;
            firstNameTextBox.IsEnabled = true;
            lastNameTextBox.IsEnabled = true;
            countryTextBox.IsEnabled = true;
            BindingOperations.ClearBinding(custIdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(countryTextBox, TextBox.TextProperty);
            custIdTextBox.Text = tempcustIdTextBox;
            firstNameTextBox.Text = tempfirstNameTextBox;
            lastNameTextBox.Text = templastNameTextBox;
            countryTextBox.Text = tempcountryTextBox;
            Keyboard.Focus(custIdTextBox);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempcustIdTextBox = custIdTextBox.Text.ToString();
            string tempfirstNameTextBox = firstNameTextBox.Text.ToString();
            string templastNameTextBox = lastNameTextBox.Text.ToString();
            string tempcountryTextBox = countryTextBox.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(custIdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(firstNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(lastNameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(countryTextBox, TextBox.TextProperty);
            custIdTextBox.Text = tempcustIdTextBox;
            firstNameTextBox.Text = tempfirstNameTextBox;
            lastNameTextBox.Text = templastNameTextBox;
            countryTextBox.Text = tempcountryTextBox;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnPrevious.IsEnabled = true;
            btnNext.IsEnabled = true;
            custIdTextBox.IsEnabled = false;
            firstNameTextBox.IsEnabled = false;
            lastNameTextBox.IsEnabled = false;
            countryTextBox.IsEnabled = false;
            custIdTextBox.SetBinding(TextBox.TextProperty, custIdTextBoxBinding);
            firstNameTextBox.SetBinding(TextBox.TextProperty, firstNameTextBoxBinding);
            lastNameTextBox.SetBinding(TextBox.TextProperty, lastNameTextBoxBinding);
            countryTextBox.SetBinding(TextBox.TextProperty, countryTextBoxBinding);
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToPrevious();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    customer = new Customer()
                    {
                        First_Name = firstNameTextBox.Text.Trim(),
                        Last_Name = lastNameTextBox.Text.Trim(),
                        Country = countryTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Customers.Add(customer);
                    customerViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                    SetValidationBinding();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    customer.First_Name = firstNameTextBox.Text.Trim();
                    customer.Last_Name = lastNameTextBox.Text.Trim();
                    customer.Country = countryTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                    SetValidationBinding();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
                // pozitionarea pe item-ul curent
                customerViewSource.View.MoveCurrentTo(customer);

            }
            else
            if (action == ActionState.Delete)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    ctx.Customers.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
            }
        }
        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            motorcycleIdTextBox.IsEnabled = true;
            colorTextBox.IsEnabled = true;
            companyTextBox.IsEnabled = true;
            priceEURTextBox.IsEnabled = true;
            BindingOperations.ClearBinding(motorcycleIdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(colorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(companyTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(priceEURTextBox, TextBox.TextProperty);
            motorcycleIdTextBox.Text = "";
            colorTextBox.Text = "";
            companyTextBox.Text = "";
            priceEURTextBox.Text = "";
            Keyboard.Focus(motorcycleIdTextBox);
        }
        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempmotorcycleIdTextBox = motorcycleIdTextBox.Text.ToString();
            string tempcolorTextBox = colorTextBox.Text.ToString();
            string tempcompanyTextBox = companyTextBox.Text.ToString();
            string temppriceEURTextBox = priceEURTextBox.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            motorcycleIdTextBox.IsEnabled = true;
            colorTextBox.IsEnabled = true;
            companyTextBox.IsEnabled = true;
            priceEURTextBox.IsEnabled = true;
            BindingOperations.ClearBinding(motorcycleIdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(colorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(companyTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(priceEURTextBox, TextBox.TextProperty);
            motorcycleIdTextBox.Text = tempmotorcycleIdTextBox;
            colorTextBox.Text = tempcolorTextBox;
            companyTextBox.Text = tempcompanyTextBox;
            priceEURTextBox.Text = temppriceEURTextBox;
            Keyboard.Focus(motorcycleIdTextBox);
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempmotorcycleIdTextBox = motorcycleIdTextBox.Text.ToString();
            string tempcolorTextBox = colorTextBox.Text.ToString();
            string tempcompanyTextBox = companyTextBox.Text.ToString();
            string temppriceEURTextBox = priceEURTextBox.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(motorcycleIdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(colorTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(companyTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(priceEURTextBox, TextBox.TextProperty);
            motorcycleIdTextBox.Text = tempmotorcycleIdTextBox;
            colorTextBox.Text = tempcolorTextBox;
            companyTextBox.Text = tempcompanyTextBox;
            priceEURTextBox.Text = temppriceEURTextBox;
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnPrevious.IsEnabled = true;
            btnNext.IsEnabled = true;
            motorcycleIdTextBox.IsEnabled = false;
            colorTextBox.IsEnabled = false;
            companyTextBox.IsEnabled = false;
            priceEURTextBox.IsEnabled = false;
            motorcycleIdTextBox.SetBinding(TextBox.TextProperty, motorcycleIdTextBoxBinding);
            colorTextBox.SetBinding(TextBox.TextProperty, colorTextBoxBinding);
            companyTextBox.SetBinding(TextBox.TextProperty, companyTextBoxBinding);
            priceEURTextBox.SetBinding(TextBox.TextProperty, priceEURTextBoxBinding);
        }
        private void btExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            catalogViewSource.View.MoveCurrentToNext();
        }
        private void btPrevious_Click(object sender, RoutedEventArgs e)
        {
            catalogViewSource.View.MoveCurrentToPrevious();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = null;
            if (action == ActionState.New)
            {
                try
                {
                    catalog = new Catalog()
                    {
                        Color = colorTextBox.Text.Trim(),
                        Company = companyTextBox.Text.Trim(),
                        Price__EUR_ = priceEURTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Catalogs.Add(catalog);
                    catalogViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                    SetValidationBinding();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catalogViewSource.View.Refresh();
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    catalog = (Catalog)catalogDataGrid.SelectedItem;
                    catalog.Color = colorTextBox.Text.Trim();
                    catalog.Company = companyTextBox.Text.Trim();
                    catalog.Price__EUR_ = priceEURTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                    SetValidationBinding();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catalogViewSource.View.Refresh();
                // pozitionarea pe item-ul curent
                catalogViewSource.View.MoveCurrentTo(catalog);

            }
            else
            if (action == ActionState.Delete)
            {
                try
                {
                    catalog = (Catalog)catalogDataGrid.SelectedItem;
                    ctx.Catalogs.Remove(catalog);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catalogViewSource.View.Refresh();
            }
        }
        private void butonNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtCatalog.IsEnabled = true;
            txtCustomers.IsEnabled = true;
            BindingOperations.ClearBinding(txtCatalog, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtCustomers, TextBox.TextProperty);
            txtCatalog.Text = "";
            txtCustomers.Text = "";
            Keyboard.Focus(txtCatalog);
        }
        private void butonEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string temptxtCatalog = txtCatalog.Text.ToString();
            string temptxtCustomers = txtCustomers.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtCatalog.IsEnabled = true;
            txtCustomers.IsEnabled = true;
            BindingOperations.ClearBinding(txtCatalog, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtCustomers, TextBox.TextProperty);
            txtCatalog.Text = temptxtCatalog;
            txtCustomers.Text = temptxtCustomers;
            Keyboard.Focus(txtCatalog);
        }
        private void butonDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string temptxtCatalog = txtCatalog.Text.ToString();
            string temptxtCustomers = txtCustomers.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(txtCatalog, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtCustomers, TextBox.TextProperty);
            txtCatalog.Text = temptxtCatalog;
            txtCustomers.Text = temptxtCustomers;
        }
        private void butonCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnPrevious.IsEnabled = true;
            btnNext.IsEnabled = true;
            txtCatalog.IsEnabled = false;
            txtCustomers.IsEnabled = false;
            txtCatalog.SetBinding(TextBox.TextProperty, txtCatalogBinding);
            txtCustomers.SetBinding(TextBox.TextProperty, txtCustomerBinding);
        }
        private void butonExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void butonNext_Click(object sender, RoutedEventArgs e)
        {
            catalogOrdersViewSource.View.MoveCurrentToNext();
        }
        private void butonPrevious_Click(object sender, RoutedEventArgs e)
        {
            catalogOrdersViewSource.View.MoveCurrentToPrevious();
        }
        private void butonSave_Click(object sender, RoutedEventArgs e)
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Catalog catalog = (Catalog)cmbCatalog.SelectedItem;
                    //instantiem Order entity
                    order = new Order()
                    {
                        CustId = customer.CustId,
                        MotorcycleId = catalog.MotorcycleId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Orders.Add(order);
                    catalogOrdersViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                    SetValidationBinding();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                customerViewSource.View.Refresh();
            }
            if (action == ActionState.Edit)
            {
                dynamic selectedOrder = ordersDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedOrder.OrderId;
                    var editedOrder = ctx.Orders.FirstOrDefault(s => s.OrderId == curr_id);
                    if (editedOrder != null)
                    {
                        editedOrder.CustId = Int32.Parse(cmbCustomers.SelectedValue.ToString());
                        editedOrder.MotorcycleId = Convert.ToInt32(cmbCatalog.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                        SetValidationBinding();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                customerViewSource.View.MoveCurrentTo(selectedOrder);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedOrder = ordersDataGrid.SelectedItem;
                    int curr_id = selectedOrder.OrderId;
                    var deletedOrder = ctx.Orders.FirstOrDefault(s => s.OrderId == curr_id);
                    if (deletedOrder != null)
                    {
                        ctx.Orders.Remove(deletedOrder);
                        ctx.SaveChanges();
                        MessageBox.Show("Order Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BindDataGrid()
        {
            var queryOrder = from ord in ctx.Orders
                             join cust in ctx.Customers on ord.CustId equals
                             cust.CustId
                             join moto in ctx.Catalogs on ord.MotorcycleId equals
                             moto.MotorcycleId
                             select new
                             {
                                 ord.OrderId,
                                 ord.MotorcycleId,
                                 ord.CustId,
                                 cust.First_Name,
                                 cust.Last_Name,
                                 cust.Country,
                                 moto.Company,
                                 moto.Color,
                                 moto.Price__EUR_
                             };
            catalogOrdersViewSource.Source = queryOrder.ToList();
        }
        private void SetValidationBinding()
        {
            Binding firstNameValidationBinding = new Binding();
            firstNameValidationBinding.Source = customerViewSource;
            firstNameValidationBinding.Path = new PropertyPath("FirstName");
            firstNameValidationBinding.NotifyOnValidationError = true;
            firstNameValidationBinding.Mode = BindingMode.TwoWay;
            firstNameValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string required
            firstNameValidationBinding.ValidationRules.Add(new StringNotEmpty());
            firstNameTextBox.SetBinding(TextBox.TextProperty, firstNameValidationBinding);
            Binding lastNameValidationBinding = new Binding();
            lastNameValidationBinding.Source = customerViewSource;
            lastNameValidationBinding.Path = new PropertyPath("LastName");
            lastNameValidationBinding.NotifyOnValidationError = true;
            lastNameValidationBinding.Mode = BindingMode.TwoWay;
            lastNameValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            lastNameValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            lastNameTextBox.SetBinding(TextBox.TextProperty, lastNameValidationBinding); //setare binding nou
        }
    }
}
