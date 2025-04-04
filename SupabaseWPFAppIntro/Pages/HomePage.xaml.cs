using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SupabaseAppIntro.Models;
using ConsoleSupabase.Data.Sourсe.Remote.SupabaseDB;

namespace SupabaseWPFAppIntro.Pages
{
    public partial class HomePage : UserControl
    {
        private readonly SupabaseService _supabaseService;
        private List<Student> _students;

        public HomePage(string email)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Loading HomePage: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            _supabaseService = new SupabaseService();
            LoadDataAsync();
        }

   private async Task LoadDataAsync()
   {
       try
       {
           await _supabaseService.InitServiceAsync();
           _supabaseService.SetAuthUser();
           _students = await _supabaseService.GetStudentsByUserId();
   
           if (_students != null)
           {
               foreach (var student in _students)
               {
                   student.Marks = await _supabaseService.GetMarksByStudentId(student.id);
               }
   
               StudentMarkDataGrid.ItemsSource = null; // Clear the ItemsSource
               StudentMarkDataGrid.ItemsSource = _students; // Set the new ItemsSource
           }
           else
           {
               MessageBox.Show("No students found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
           }
       }
       catch (Exception ex)
       {
           MessageBox.Show($"Error Loading Data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
       }
   }
   
   private async void OnAddButtonClick(object sender, RoutedEventArgs e)
   {
       try
       {
           var studentName = StudentNameTextBox.Text;
           if (int.TryParse(MarkTextBox.Text, out int markValue) && !string.IsNullOrEmpty(studentName) && markValue > 0)
           {
               var studentAdded = await _supabaseService.AddStudent(studentName);
               if (studentAdded)
               {
                   var student = _students.Find(s => s.Name == studentName);
                   if (student != null)
                   {
                       await _supabaseService.AddMark(markValue, student.id);
                       await LoadDataAsync(); // Call LoadDataAsync to refresh the data grid
                   }
               }
           }
       }
       catch (Exception ex)
       {
           MessageBox.Show($"Error adding data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
       }
   }
    }
}