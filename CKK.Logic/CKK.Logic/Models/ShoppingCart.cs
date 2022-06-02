﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Models
{
    public class ShoppingCart
    {
        private Customer _customer;
        private List<ShoppingCartItem> _products;

        public ShoppingCart(Customer customer)
        {
            _customer = customer;
            _products = new List<ShoppingCartItem>();
        }

        public int GetCustomerId()
        {
            return  _customer.GetId();
        }

        public ShoppingCartItem GetProductById(int id) 
        {
            var prodId =
                from i in _products
                let productActual = i.GetProduct()
                where productActual.GetId() == id
                select i;

            return (ShoppingCartItem)prodId;
                                      
        }

        public ShoppingCartItem AddProduct(Product prod, int quantity)
        {
            var item = new ShoppingCartItem(prod, quantity);
            var itemQ = item.GetQuantity();

            if (_products.Contains(item))
            {
                item.SetQuantity(itemQ + quantity);
            }
            else { _products.Add(item); }

            return item;
                                                                                          
        }


        public ShoppingCartItem RemoveProduct(int id, int quantity)
        {
            var prod = GetProductById(id);
            var prodIn = _products.IndexOf(prod);
            var prodQ = _products[prodIn].GetQuantity();
            _products[prodIn].SetQuantity(prodQ - quantity);

            if(_products[prodIn].GetQuantity() <= 0)
            {
                _products[prodIn].SetQuantity(0);
                _products.RemoveAt(prodIn);
            }

            return prod;
                      
        }

        public decimal GetTotal()
        {
            var param = _products.Count();
            decimal total = 0;
            

            for(int count = 0; count <= param; count++)
            {
                var price = _products[count].GetTotal();
                total += price;
            }

            return total;
            
        }

        public List<ShoppingCartItem> GetProducts ()
        {

            var itemsSorted =
                from i in _products
                orderby i
                select i;

            return (List<ShoppingCartItem>) itemsSorted;
           
        }
        
    }
}
