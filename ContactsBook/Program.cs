using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsBookApp
{
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Contact(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Phone: {Phone}, Email: {Email}, Address: {Address}";
        }
    }

    public class ContactsBook
    {
        private List<Contact> contacts = new List<Contact>();

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMenuTitle();

                Console.WriteLine("1. Add a New Contact");
                Console.WriteLine("2. Edit a Contact");
                Console.WriteLine("3. Search Contacts");
                Console.WriteLine("4. List All Contacts");
                Console.WriteLine("5. Remove a Contact");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        EditContact();
                        break;
                    case "3":
                        SearchContacts();
                        break;
                    case "4":
                        ListContacts();
                        break;
                    case "5":
                        RemoveContact();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
            Console.WriteLine("Exiting Contacts Book. Goodbye!");
        }


        private void ShowMenuTitle()
        {
            string asciiArt = @"

 ██████  ██████  ███    ██ ████████  █████   ██████ ████████ ███████     ██████   ██████   ██████  ██   ██ 
██      ██    ██ ████   ██    ██    ██   ██ ██         ██    ██          ██   ██ ██    ██ ██    ██ ██  ██  
██      ██    ██ ██ ██  ██    ██    ███████ ██         ██    ███████     ██████  ██    ██ ██    ██ █████   
██      ██    ██ ██  ██ ██    ██    ██   ██ ██         ██         ██     ██   ██ ██    ██ ██    ██ ██  ██  
 ██████  ██████  ██   ████    ██    ██   ██  ██████    ██    ███████     ██████   ██████   ██████  ██   ██ 
                                                                                                        
";
            Console.WriteLine(asciiArt);
            Console.WriteLine();
        }


        private void AddContact()
        {
            Console.WriteLine("\n-- Add a New Contact --");
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            var existing = contacts.FirstOrDefault(c =>
                c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                c.Address.Equals(address, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.Name = name;
                existing.Phone = phone;
                existing.Email = email;
                existing.Address = address;
                Console.WriteLine("Contact updated successfully (duplicate found).");
            }
            else
            {
                contacts.Add(new Contact(name, phone, email, address));
                Console.WriteLine("Contact added successfully.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        private void EditContact()
        {
            Console.WriteLine("\n-- Edit a Contact --");
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available to edit.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            ListContacts();
            Console.Write("Enter the number of the contact to edit: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index >= 1 && index <= contacts.Count)
            {

                Contact contact = contacts[index - 1];
                Console.WriteLine($"\nEditing Contact: {contact}");
                Console.WriteLine("Press Enter without typing anything to leave a field unchanged.");

                Console.Write("New Name (current: " + contact.Name + "): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    contact.Name = newName;

                Console.Write("New Phone (current: " + contact.Phone + "): ");
                string newPhone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPhone))
                    contact.Phone = newPhone;

                Console.Write("New Email (current: " + contact.Email + "): ");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail))
                    contact.Email = newEmail;

                Console.Write("New Address (current: " + contact.Address + "): ");
                string newAddress = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newAddress))
                    contact.Address = newAddress;

                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid contact number; no changes were made.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void SearchContacts()
        {
            Console.WriteLine("\n-- Search Contacts --");
            Console.Write("Enter search term (Name): ");
            string searchTerm = Console.ReadLine().ToLower();

            List<Contact> results = contacts
                .Where(c => c.Name.ToLower().Contains(searchTerm))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No contacts found with that search term.");
            }
            else
            {
                Console.WriteLine("Search Results:");
                foreach (Contact c in results)
                {
                    Console.WriteLine(c);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListContacts()
        {
            Console.WriteLine("\n-- List All Contacts --");
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
            }
            else
            {
                int index = 1;
                foreach (Contact c in contacts)
                {
                    Console.WriteLine($"{index++}. {c}");
                }
            }
        }

        private void RemoveContact()
        {
            Console.WriteLine("\n-- Remove a Contact --");
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts to remove.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            ListContacts();
            Console.Write("Enter the number of the contact to remove: ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index >= 1 && index <= contacts.Count)
            {
                contacts.RemoveAt(index - 1);
                Console.WriteLine("Contact removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid index; no contact was removed.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ContactsBook app = new ContactsBook();
            app.Run();
        }
    }
}

