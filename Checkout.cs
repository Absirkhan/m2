using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace m2
{
    public partial class Checkout : Form
    {
        private Customer currentCustomer;
        public Checkout(Customer customer)
        {
            this.currentCustomer= customer;
            InitializeComponent();
        }

        private void Checkout_Load(object sender, EventArgs e)
        {
            decimal totalPrice = 0;
            StringBuilder summary = new StringBuilder();

            // Connection string
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";
            string query = @"
        SELECT p.ProductName, p.UnitPrice, c.Quantity, p.ShippingOptions
        FROM Cart c
        INNER JOIN Product p ON c.ProductID = p.ProductID
        WHERE c.CustomerID = @CustomerID";



            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Pass the current customer ID to the query
                        cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Loop through each item in the cart
                            while (reader.Read())
                            {
                                string productName = reader["ProductName"].ToString();
                                decimal unitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                                int quantity = Convert.ToInt32(reader["Quantity"]);
                                string shippingOption = reader["ShippingOptions"].ToString();

                                decimal totalProductPrice = unitPrice * quantity;
                                totalPrice += totalProductPrice;

                                // Add product details to summary
                                summary.AppendLine($"{productName} - {unitPrice:C} x {quantity} = {totalProductPrice:C} ({shippingOption})");
                            }
                        }
                    }

                    // Display the summary of cart items
                    //textBox1.Text = summary.ToString();

                    // Display the total price (formatted as currency)
                    label1.Text = $"Total: {totalPrice:C}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while calculating the total price: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal totalOrderPrice = 0;
            string connectionString = "Data Source=AbsirAhmedKhan;Initial Catalog=m3;Integrated Security=True";

            string orderQuery = @"
            INSERT INTO [Order] (CustomerID, ProductID, OrderDate, Status, TotalPrice, PaymentMethod)
            VALUES (@CustomerID, @ProductID, GETDATE(), 'Pending', @TotalPrice, @PaymentMethod);
            SELECT SCOPE_IDENTITY();"; // Get the OrderID


            // Query to insert order details
            string orderDetailsQuery = @"
        INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, TotalPrice)
        VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @TotalPrice);";

            string orderHistoryQuery = @"
        INSERT INTO OrderHistory (OrderID, ProductID, CustomerID, Bill, OrderDate)
        VALUES (@OrderID, @ProductID, @CustomerID, @Bill, GETDATE());";


            // Query to clear the cart
            string clearCartQuery = "DELETE FROM Cart WHERE CustomerID = @CustomerID;";


            string updateProductStockQuery = @"
            UPDATE Product
            SET Quantity = Quantity - @Quantity
            WHERE ProductID = @ProductID AND Quantity >= @Quantity;";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Step 1: Calculate the total price for the order
                    string cartQuery = @"
                SELECT p.ProductID, p.UnitPrice, c.Quantity
                FROM Cart c
                INNER JOIN Product p ON c.ProductID = p.ProductID
                WHERE c.CustomerID = @CustomerID";

                    List<dynamic> cartItems = new List<dynamic>(); // Store cart items temporarily


                    using (SqlCommand cmd = new SqlCommand(cartQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productID = Convert.ToInt32(reader["ProductID"]);
                                decimal unitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                                int quantity = Convert.ToInt32(reader["Quantity"]);
                                decimal itemTotalPrice = unitPrice * quantity;

                                totalOrderPrice += itemTotalPrice;

                                cartItems.Add(new
                                {
                                    ProductID = productID,
                                    UnitPrice = unitPrice,
                                    Quantity = quantity,
                                    TotalPrice = itemTotalPrice
                                });
                            }
                        }
                    }

                    if (cartItems.Count == 0)
                    {
                        MessageBox.Show("Your cart is empty. Please add items to your cart before placing an order.", "Cart Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Stop further execution
                    }
                    else
                    {
                        MessageBox.Show($"Cart contains {cartItems.Count} items.", "Cart Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Step 2: Insert into Order table
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        int orderID;
                        int firstProductID = cartItems.Count > 0 ? cartItems[0].ProductID : 0; // Get the first product in the cart


                        using (SqlCommand cmd = new SqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);
                            cmd.Parameters.AddWithValue("@ProductID", firstProductID);
                            cmd.Parameters.AddWithValue("@TotalPrice", totalOrderPrice);
                            cmd.Parameters.AddWithValue("@PaymentMethod", "Credit Card"); // Example payment method

                            // Execute and get the OrderID
                            orderID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Step 3: Insert each product into OrderDetails
                        using (SqlCommand cmdDetails = new SqlCommand(orderDetailsQuery, conn, transaction))
                        {
                            cmdDetails.Parameters.Add("@OrderID", SqlDbType.Int);
                            cmdDetails.Parameters.Add("@ProductID", SqlDbType.Int);
                            cmdDetails.Parameters.Add("@Quantity", SqlDbType.Int);
                            cmdDetails.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                            cmdDetails.Parameters.Add("@TotalPrice", SqlDbType.Decimal);

                            foreach (var item in cartItems)
                            {
                                cmdDetails.Parameters["@OrderID"].Value = orderID;
                                cmdDetails.Parameters["@ProductID"].Value = item.ProductID;
                                cmdDetails.Parameters["@Quantity"].Value = item.Quantity;
                                cmdDetails.Parameters["@UnitPrice"].Value = item.UnitPrice;
                                cmdDetails.Parameters["@TotalPrice"].Value = item.TotalPrice;

                                cmdDetails.ExecuteNonQuery();
                            }
                        }

                        // Step 4: Clear the cart
                        using (SqlCommand cmdClear = new SqlCommand(clearCartQuery, conn, transaction))
                        {
                            cmdClear.Parameters.AddWithValue("@CustomerID", currentCustomer.CustomerID);
                            cmdClear.ExecuteNonQuery();
                        }

                        // Step 5: Insert into OrderHistory for each product
                        using (SqlCommand cmdHistory = new SqlCommand(orderHistoryQuery, conn, transaction))
                        {
                            cmdHistory.Parameters.Add("@OrderID", SqlDbType.Int);
                            cmdHistory.Parameters.Add("@ProductID", SqlDbType.Int);
                            cmdHistory.Parameters.Add("@CustomerID", SqlDbType.Int);
                            cmdHistory.Parameters.Add("@Bill", SqlDbType.Decimal);

                            foreach (var item in cartItems)
                            {
                                cmdHistory.Parameters["@OrderID"].Value = orderID;
                                cmdHistory.Parameters["@ProductID"].Value = item.ProductID;
                                cmdHistory.Parameters["@CustomerID"].Value = currentCustomer.CustomerID;
                                cmdHistory.Parameters["@Bill"].Value = item.TotalPrice;

                                cmdHistory.ExecuteNonQuery();
                            }
                        }

                        // Step 6: Update Product Stock
                        using (SqlCommand cmdUpdateStock = new SqlCommand(updateProductStockQuery, conn, transaction))
                        {
                            cmdUpdateStock.Parameters.Add("@ProductID", SqlDbType.Int);
                            cmdUpdateStock.Parameters.Add("@Quantity", SqlDbType.Int);

                            foreach (var item in cartItems)
                            {
                                cmdUpdateStock.Parameters["@ProductID"].Value = item.ProductID;
                                cmdUpdateStock.Parameters["@Quantity"].Value = item.Quantity;

                                int rowsAffected = cmdUpdateStock.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception($"Insufficient stock for ProductID {item.ProductID}. Please update stock before placing an order.");
                                }
                            }
                        }


                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Your order has been placed successfully. Thank you for your purchase!");

                        // Close the checkout form and navigate back to Customer Options
                        CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
                        this.Hide();
                        coc.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while placing the order:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerOptionChoice coc = new CustomerOptionChoice(currentCustomer);
            this.Hide();
            coc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SubmitReviews sr = new SubmitReviews(currentCustomer);
            this.Hide();
            sr.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
