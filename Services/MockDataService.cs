using System;
using System.Collections.Generic;
using System.Linq;
using UI.Models;

namespace UI.Services
{
    public class MockDataService
    {
        public List<MenuItem> MenuItems { get; private set; } = new();
        public List<Order> Orders { get; private set; } = new();
        public List<Reservation> Reservations { get; private set; } = new();
        public List<InventoryItem> Inventory { get; private set; } = new();
        public List<Employee> Employees { get; private set; } = new();
        public List<FinanceRecord> FinanceRecords { get; private set; } = new();
        public List<Notification> Notifications { get; private set; } = new();

        public event Action? OnChange;

        public MockDataService()
        {
            InitializeMockData();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

        private void InitializeMockData()
        {
            // 1. Menu Items
            MenuItems = new List<MenuItem>
            {
                new() { Id = "m1", Name = "Signature Espresso", Category = "Coffee", Price = 3.50m, ImageUrl = "https://images.unsplash.com/photo-151097252790b-a638d5216123?w=300", InStock = true, StockCount = 150 },
                new() { Id = "m2", Name = "Cafe Latte", Category = "Coffee", Price = 4.50m, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?w=300", InStock = true, StockCount = 80 },
                new() { Id = "m3", Name = "Vanilla Flat White", Category = "Coffee", Price = 4.75m, ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=300", InStock = true, StockCount = 60 },
                new() { Id = "m4", Name = "Nitro Cold Brew", Category = "Coffee", Price = 5.00m, ImageUrl = "https://images.unsplash.com/photo-1447933601403-0c6688de566e?w=300", InStock = true, StockCount = 40 },
                new() { Id = "m5", Name = "Butter Croissant", Category = "Bakery", Price = 3.75m, ImageUrl = "https://images.unsplash.com/photo-1483695028939-5bb13f8648b0?w=300", InStock = true, StockCount = 12 },
                new() { Id = "m6", Name = "Almond Pain au Chocolat", Category = "Bakery", Price = 4.25m, ImageUrl = "https://images.unsplash.com/photo-1509440159596-0249088772ff?w=300", InStock = true, StockCount = 4 }, // Low Stock!
                new() { Id = "m7", Name = "Blueberry Danish", Category = "Bakery", Price = 4.00m, ImageUrl = "https://images.unsplash.com/photo-1608686207856-001b95cf60ca?w=300", InStock = true, StockCount = 8 },
                new() { Id = "m8", Name = "Matcha Strawberry Mochi Cake", Category = "Dessert", Price = 6.50m, ImageUrl = "https://images.unsplash.com/photo-1565958011703-44f9829ba187?w=300", InStock = true, StockCount = 15 },
                new() { Id = "m9", Name = "Espresso Tiramisu", Category = "Dessert", Price = 7.00m, ImageUrl = "https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?w=300", InStock = true, StockCount = 10 }
            };

            // 2. Inventory Items
            Inventory = new List<InventoryItem>
            {
                new() { Id = "i1", Name = "Organic Espresso Beans (Single Origin)", SKU = "INV-BEANS-01", Category = "Coffee", StockLevel = 42, ReorderPoint = 15, Unit = "kg", CostPrice = 12.50m, SellingPrice = 45.00m },
                new() { Id = "i2", Name = "Oat Milk (Barista Edition)", SKU = "INV-MILK-OAT", Category = "Dairy/Milk", StockLevel = 8, ReorderPoint = 12, Unit = "L", CostPrice = 2.10m, SellingPrice = 0.00m }, // Under Stock!
                new() { Id = "i3", Name = "Whole Milk (Organic)", SKU = "INV-MILK-WHOLE", Category = "Dairy/Milk", StockLevel = 36, ReorderPoint = 10, Unit = "L", CostPrice = 1.40m, SellingPrice = 0.00m },
                new() { Id = "i4", Name = "Pastry Flour (Unbleached)", SKU = "INV-BAKE-FLOUR", Category = "Baking", StockLevel = 50, ReorderPoint = 20, Unit = "kg", CostPrice = 0.95m, SellingPrice = 0.00m },
                new() { Id = "i5", Name = "Matcha Powder (Ceremonial Grade)", SKU = "INV-MATCHA-01", Category = "Tea", StockLevel = 2, ReorderPoint = 3, Unit = "kg", CostPrice = 75.00m, SellingPrice = 220.00m } // Under Stock!
            };

            // 3. Pre-populated Orders
            Orders = new List<Order>
            {
                new()
                {
                    Id = "CS-1001",
                    TicketNumber = "01",
                    CustomerName = "Marc Andreessen",
                    CreatedAt = DateTime.Now.AddMinutes(-32),
                    Subtotal = 12.00m,
                    Tax = 0.96m,
                    Discount = 0.00m,
                    Total = 12.96m,
                    Status = OrderStatus.Completed,
                    PaymentMethod = "Card",
                    Items = new List<OrderItem>
                    {
                        new() { Item = MenuItems[0], Quantity = 2, Status = OrderItemStatus.Completed },
                        new() { Item = MenuItems[4], Quantity = 1, Status = OrderItemStatus.Completed }
                    }
                },
                new()
                {
                    Id = "CS-1002",
                    TicketNumber = "02",
                    CustomerName = "Jack Dorsey",
                    CreatedAt = DateTime.Now.AddMinutes(-18),
                    Subtotal = 13.75m,
                    Tax = 1.10m,
                    Discount = 0.00m,
                    Total = 14.85m,
                    Status = OrderStatus.Preparing,
                    PaymentMethod = "Tap",
                    Items = new List<OrderItem>
                    {
                        new() { Item = MenuItems[1], Quantity = 1, Notes = "Extra hot, Oat Milk", Status = OrderItemStatus.Preparing },
                        new() { Item = MenuItems[8], Quantity = 1, Status = OrderItemStatus.Pending }
                    }
                },
                new()
                {
                    Id = "CS-1003",
                    TicketNumber = "03",
                    CustomerName = "Paul Graham",
                    CreatedAt = DateTime.Now.AddMinutes(-5),
                    Subtotal = 11.25m,
                    Tax = 0.90m,
                    Discount = 1.00m,
                    Total = 11.15m,
                    Status = OrderStatus.Pending,
                    PaymentMethod = "Cash",
                    Items = new List<OrderItem>
                    {
                        new() { Item = MenuItems[3], Quantity = 1, Status = OrderItemStatus.Pending },
                        new() { Item = MenuItems[7], Quantity = 1, Status = OrderItemStatus.Pending }
                    }
                }
            };

            // 4. Reservations
            Reservations = new List<Reservation>
            {
                new() { Id = "R-201", CustomerName = "Elon Musk", TimeSlot = "09:00 AM", TableNumber = 4, Covers = 2, Status = ReservationStatus.Seated, Notes = "Prefers window table" },
                new() { Id = "R-202", CustomerName = "Gwynne Shotwell", TimeSlot = "11:30 AM", TableNumber = 8, Covers = 4, Status = ReservationStatus.Confirmed, Notes = "Business meeting" },
                new() { Id = "R-203", CustomerName = "Sam Altman", TimeSlot = "02:00 PM", TableNumber = 2, Covers = 1, Status = ReservationStatus.Confirmed, Notes = "Quick coffee discussion" },
                new() { Id = "R-204", CustomerName = "Jensen Huang", TimeSlot = "07:30 PM", TableNumber = 12, Covers = 6, Status = ReservationStatus.Confirmed, Notes = "Celebration dinner. Bring signature dessert." }
            };

            // 5. Employees
            Employees = new List<Employee>
            {
                new() { Id = "e1", Name = "Elena Rostova", Role = "Head Barista", ImageUrl = "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=100", IsOnDuty = true, ActiveShift = "07:00 AM - 03:00 PM", HoursWorkedThisWeek = 38.5m },
                new() { Id = "e2", Name = "Hiroshi Tanaka", Role = "Barista / Cashier", ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100", IsOnDuty = true, ActiveShift = "08:00 AM - 04:00 PM", HoursWorkedThisWeek = 34.0m },
                new() { Id = "e3", Name = "Sophia Gallagher", Role = "Pastry Chef", ImageUrl = "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=100", IsOnDuty = false, ActiveShift = "05:00 AM - 01:00 PM", HoursWorkedThisWeek = 40.0m },
                new() { Id = "e4", Name = "Marcus Vance", Role = "Shift Lead", ImageUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=100", IsOnDuty = true, ActiveShift = "02:00 PM - 10:00 PM", HoursWorkedThisWeek = 42.5m }
            };

            // 6. Finance Records
            FinanceRecords = new List<FinanceRecord>
            {
                new() { Id = "f1", Type = "Revenue", Category = "Sales", Amount = 485.60m, Timestamp = DateTime.Now.AddDays(-2), Description = "Daily POS Sales (Thursday)" },
                new() { Id = "f2", Type = "Expense", Category = "Inventory", Amount = 120.00m, Timestamp = DateTime.Now.AddDays(-2), Description = "Dairy Delivery" },
                new() { Id = "f3", Type = "Revenue", Category = "Sales", Amount = 612.45m, Timestamp = DateTime.Now.AddDays(-1), Description = "Daily POS Sales (Friday)" },
                new() { Id = "f4", Type = "Expense", Category = "Utilities", Amount = 350.00m, Timestamp = DateTime.Now.AddDays(-1), Description = "Electricity Bill" },
                new() { Id = "f5", Type = "Revenue", Category = "Sales", Amount = 38.96m, Timestamp = DateTime.Now, Description = "POS Orders CS-1001" }
            };

            // 7. Initial Notifications
            Notifications = new List<Notification>
            {
                new() { Id = "n1", Title = "Low Stock Warning", Message = "Almond Pain au Chocolat is running low (4 items left)", Type = "warning", Timestamp = DateTime.Now.AddMinutes(-45), IsRead = false },
                new() { Id = "n2", Title = "Critical Supply Deficit", Message = "Oat Milk (Barista Edition) is below reorder point (8L left)", Type = "error", Timestamp = DateTime.Now.AddMinutes(-30), IsRead = false },
                new() { Id = "n3", Title = "New Reservation", Message = "Jensen Huang booked Table 12 for 07:30 PM (6 covers)", Type = "info", Timestamp = DateTime.Now.AddMinutes(-10), IsRead = true }
            };
        }

        // --- Core Methods ---

        public Order PlaceOrder(List<OrderItem> items, string customerName, string paymentMethod, decimal discount = 0m)
        {
            var id = $"CS-{1000 + Orders.Count + 1}";
            var ticketNo = (Orders.Count + 1).ToString("D2");

            var subtotal = items.Sum(i => i.Item.Price * i.Quantity);
            var tax = subtotal * 0.08m; // 8% sales tax
            var total = subtotal + tax - discount;

            var newOrder = new Order
            {
                Id = id,
                TicketNumber = ticketNo,
                CustomerName = string.IsNullOrWhiteSpace(customerName) ? "Guest" : customerName,
                CreatedAt = DateTime.Now,
                Subtotal = subtotal,
                Tax = tax,
                Discount = discount,
                Total = total,
                Status = OrderStatus.Pending,
                PaymentMethod = paymentMethod,
                Items = items.Select(i => new OrderItem
                {
                    Item = i.Item,
                    Quantity = i.Quantity,
                    Notes = i.Notes,
                    Status = OrderItemStatus.Pending
                }).ToList()
            };

            Orders.Add(newOrder);

            // Deduct stock counts in menu list & inventory levels
            foreach (var item in items)
            {
                var menuRef = MenuItems.FirstOrDefault(m => m.Id == item.Item.Id);
                if (menuRef != null)
                {
                    menuRef.StockCount = Math.Max(0, menuRef.StockCount - item.Quantity);
                    if (menuRef.StockCount <= 5)
                    {
                        AddNotification("Low Stock Warning", $"{menuRef.Name} is running low ({menuRef.StockCount} items left)", "warning");
                    }
                }

                // Simulate ingredient deduction in raw inventory
                if (item.Item.Category == "Coffee")
                {
                    var coffeeBeans = Inventory.FirstOrDefault(i => i.Id == "i1");
                    if (coffeeBeans != null)
                    {
                        coffeeBeans.StockLevel = Math.Max(0, coffeeBeans.StockLevel - (item.Quantity * 18 / 1000)); // 18g per shot
                    }
                }
            }

            // Track Revenue
            FinanceRecords.Add(new FinanceRecord
            {
                Id = $"f{FinanceRecords.Count + 1}",
                Type = "Revenue",
                Category = "Sales",
                Amount = total,
                Timestamp = DateTime.Now,
                Description = $"POS Order {id}"
            });

            AddNotification("Order Received", $"New order {id} (Ticket #{ticketNo}) for {newOrder.CustomerName} - ${total:F2}", "success");

            NotifyStateChanged();
            return newOrder;
        }

        public void UpdateOrderStatus(string orderId, OrderStatus status)
        {
            var order = Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = status;

                // Sync item statuses
                if (status == OrderStatus.Completed)
                {
                    order.Items.ForEach(i => i.Status = OrderItemStatus.Completed);
                }
                else if (status == OrderStatus.Preparing)
                {
                    order.Items.ForEach(i => i.Status = OrderItemStatus.Preparing);
                }

                NotifyStateChanged();
            }
        }

        public void BumpKitchenTicket(string orderId)
        {
            var order = Orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                if (order.Status == OrderStatus.Pending)
                {
                    order.Status = OrderStatus.Preparing;
                    order.Items.ForEach(i => i.Status = OrderItemStatus.Preparing);
                    AddNotification("Kitchen Update", $"Order {orderId} is now preparing", "info");
                }
                else if (order.Status == OrderStatus.Preparing)
                {
                    order.Status = OrderStatus.Ready;
                    order.Items.ForEach(i => i.Status = OrderItemStatus.Ready);
                    AddNotification("Kitchen Update", $"Order {orderId} (Ticket #{order.TicketNumber}) is Ready for pickup!", "success");
                }
                else if (order.Status == OrderStatus.Ready)
                {
                    order.Status = OrderStatus.Completed;
                    order.Items.ForEach(i => i.Status = OrderItemStatus.Completed);
                    AddNotification("Order Picked Up", $"Order {orderId} completed", "success");
                }

                NotifyStateChanged();
            }
        }

        public void AddReservation(string name, string timeSlot, int tableNumber, int covers, string notes = "")
        {
            var newRes = new Reservation
            {
                Id = $"R-{200 + Reservations.Count + 1}",
                CustomerName = name,
                TimeSlot = timeSlot,
                TableNumber = tableNumber,
                Covers = covers,
                Status = ReservationStatus.Confirmed,
                Notes = notes
            };

            Reservations.Add(newRes);
            AddNotification("New Reservation", $"{name} booked Table {tableNumber} at {timeSlot}", "info");
            NotifyStateChanged();
        }

        public void UpdateReservationStatus(string resId, ReservationStatus status)
        {
            var res = Reservations.FirstOrDefault(r => r.Id == resId);
            if (res != null)
            {
                res.Status = status;
                NotifyStateChanged();
            }
        }

        public void RestockInventoryItem(string itemId, int qty)
        {
            var invItem = Inventory.FirstOrDefault(i => i.Id == itemId);
            if (invItem != null)
            {
                invItem.StockLevel += qty;
                AddNotification("Inventory Restocked", $"Restocked {qty} {invItem.Unit} of {invItem.Name}", "success");
                NotifyStateChanged();
            }
        }

        public void ToggleEmployeeShift(string empId)
        {
            var emp = Employees.FirstOrDefault(e => e.Id == empId);
            if (emp != null)
            {
                emp.IsOnDuty = !emp.IsOnDuty;
                var dutyStatus = emp.IsOnDuty ? "checked in" : "checked out";
                AddNotification("Staff Attendance", $"{emp.Name} is now {dutyStatus}", "info");
                NotifyStateChanged();
            }
        }

        public void AddNotification(string title, string message, string type)
        {
            Notifications.Insert(0, new Notification
            {
                Id = $"n{Guid.NewGuid().ToString().Substring(0, 4)}",
                Title = title,
                Message = message,
                Type = type,
                Timestamp = DateTime.Now,
                IsRead = false
            });
            NotifyStateChanged();
        }

        public void MarkAllNotificationsAsRead()
        {
            Notifications.ForEach(n => n.IsRead = true);
            NotifyStateChanged();
        }

        // --- AI Raycast Command Parser ---
        public class AiResponse
        {
            public string Answer { get; set; } = string.Empty;
            public string Suggestion { get; set; } = string.Empty;
            public bool ActionTriggered { get; set; }
            public string TargetRoute { get; set; } = string.Empty;
        }

        public AiResponse ExecuteAiCommand(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new AiResponse { Answer = "How can I help you manage CafeSphere today?" };

            var text = query.Trim().ToLower();

            // Direct route commands
            if (text == "/pos" || text == "go to pos" || text == "open pos")
                return new AiResponse { Answer = "Opening Point of Sale...", ActionTriggered = true, TargetRoute = "pos" };

            if (text == "/kitchen" || text == "go to kitchen" || text == "open kitchen" || text == "kds")
                return new AiResponse { Answer = "Opening Kitchen Display System (KDS)...", ActionTriggered = true, TargetRoute = "kitchen" };

            if (text == "/reservations" || text == "go to reservations" || text == "open reservations")
                return new AiResponse { Answer = "Opening Table Bookings...", ActionTriggered = true, TargetRoute = "reservations" };

            if (text == "/inventory" || text == "go to inventory" || text == "inventory")
                return new AiResponse { Answer = "Loading Inventory Management...", ActionTriggered = true, TargetRoute = "inventory" };

            if (text == "/settings" || text == "settings")
                return new AiResponse { Answer = "Opening CafeSphere Settings...", ActionTriggered = true, TargetRoute = "settings" };

            // Query stats
            if (text.Contains("revenue") || text.Contains("sales") || text.Contains("money"))
            {
                var todayRev = Orders.Where(o => o.CreatedAt.Date == DateTime.Today).Sum(o => o.Total);
                var totalRev = FinanceRecords.Where(f => f.Type == "Revenue").Sum(f => f.Amount);
                return new AiResponse
                {
                    Answer = $"Today's revenue is **${todayRev:F2}** across {Orders.Count(o => o.CreatedAt.Date == DateTime.Today)} orders. Cumulative system revenue is **${totalRev:F2}**.",
                    Suggestion = "Try asking: 'how many active orders do we have?'"
                };
            }

            if (text.Contains("active orders") || text.Contains("orders queue") || text.Contains("pending orders"))
            {
                var active = Orders.Count(o => o.Status != OrderStatus.Completed && o.Status != OrderStatus.Cancelled);
                var preparing = Orders.Count(o => o.Status == OrderStatus.Preparing);
                return new AiResponse
                {
                    Answer = $"There are currently **{active} active orders** in the queue. {preparing} are currently being prepared in the kitchen.",
                    Suggestion = "You can jump to the Kitchen queue by typing '/kitchen'"
                };
            }

            if (text.Contains("low stock") || text.Contains("inventory alerts") || text.Contains("warnings"))
            {
                var lowStock = Inventory.Where(i => i.StockLevel <= i.ReorderPoint).ToList();
                if (!lowStock.Any())
                {
                    return new AiResponse { Answer = "All raw ingredients are stocked above safe thresholds! Excellent.", Suggestion = "Type '/inventory' to inspect levels." };
                }
                else
                {
                    var itemsText = string.Join(", ", lowStock.Select(i => $"{i.Name} ({i.StockLevel}{i.Unit} left)"));
                    return new AiResponse
                    {
                        Answer = $"Warning: We have **{lowStock.Count} items** below reorder thresholds: {itemsText}.",
                        Suggestion = "Would you like me to auto-place restock orders?"
                    };
                }
            }

            if (text.Contains("reserve") || text.Contains("book table") || text.Contains("new booking"))
            {
                // Simulate table reservation
                AddReservation("Walk-in Customer", "08:00 PM", 6, 2, "AI Auto-booking");
                return new AiResponse
                {
                    Answer = "Successfully booked Table 6 at 08:00 PM for a guest walk-in.",
                    Suggestion = "Opening the Reservations map...",
                    ActionTriggered = true,
                    TargetRoute = "reservations"
                };
            }

            // Fallback response
            return new AiResponse
            {
                Answer = $"I'm not fully sure how to process '{query}', but I can search records for it. Can you clarify?",
                Suggestion = "Tip: Try commands like '/pos', '/kitchen', 'sales report', or 'low stock'."
            };
        }
    }
}
