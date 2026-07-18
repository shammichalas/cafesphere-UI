using System;
using System.Collections.Generic;

namespace UI.Models
{
    public class MenuItem
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool InStock { get; set; } = true;
        public int StockCount { get; set; }
    }

    public class OrderItem
    {
        public MenuItem Item { get; set; } = new();
        public int Quantity { get; set; }
        public string Notes { get; set; } = string.Empty;
        public OrderItemStatus Status { get; set; } = OrderItemStatus.Pending;
    }

    public enum OrderItemStatus
    {
        Pending,
        Preparing,
        Ready,
        Completed
    }

    public class Order
    {
        public string Id { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string CustomerName { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "Card"; // Card, Cash, Tap
    }

    public enum OrderStatus
    {
        Pending,
        Preparing,
        Ready,
        Completed,
        Cancelled
    }

    public class Reservation
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
        public int TableNumber { get; set; }
        public int Covers { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Confirmed;
        public string Notes { get; set; } = string.Empty;
    }

    public enum ReservationStatus
    {
        Confirmed,
        Seated,
        Completed,
        Cancelled
    }

    public class InventoryItem
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int StockLevel { get; set; }
        public int ReorderPoint { get; set; }
        public string Unit { get; set; } = "pcs";
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }

    public class Employee
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsOnDuty { get; set; }
        public string ActiveShift { get; set; } = string.Empty; // e.g. "08:00 AM - 04:00 PM"
        public decimal HoursWorkedThisWeek { get; set; }
    }

    public class FinanceRecord
    {
        public string Id { get; set; } = string.Empty;
        public string Type { get; set; } = "Revenue"; // Revenue, Expense, Payout
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
    }

    public class Notification
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "info"; // info, success, warning, error
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
    }
}
