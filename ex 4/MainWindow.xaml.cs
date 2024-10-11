using System; 
using System.Collections.Generic; 
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

namespace ex_4 
{
    // Класс, представляющий элемент данных с датой и описанием
    public class DataItem
    {
        // Свойство для хранения даты элемента
        public DateTime Date { get; set; }

        // Свойство для хранения описания элемента
        public string Description { get; set; }

        // Конструктор класса DataItem, принимающий дату и описание
        public DataItem(DateTime date, string description)
        {
            Date = date; // Инициализация даты
            Description = description; // Инициализация описания
        }

        // Переопределение метода ToString для удобного вывода данных
        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Description}"; // Возвращаем дату и описание в виде строки
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // Основной класс окна WPF приложения
    {
        // Поле для хранения всех элементов данных
        private List<DataItem> allDataItems;

        // Поле для хранения отфильтрованных элементов данных
        private List<DataItem> filteredDataItems;

        // Конструктор класса MainWindow
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов, определенных в XAML
            LoadData(); // Загрузка данных при запуске приложения
        }

        // Метод для загрузки данных в список элементов
        private void LoadData()
        {
            // Инициализация списка с данными
            allDataItems = new List<DataItem>
            {
                new DataItem(new DateTime(2024, 10, 1), "Отчет о продажах"), // Добавляем первый элемент
                new DataItem(new DateTime(2024, 10, 2), "Собрание команды"), // Добавляем второй элемент
                new DataItem(new DateTime(2024, 10, 1), "Подготовка документов"), // Добавляем третий элемент
                new DataItem(new DateTime(2024, 10, 3), "Встреча с клиентом") // Добавляем четвертый элемент
            };

            // Копируем все элементы данных в отфильтрованный список (пока фильтр не применен)
            filteredDataItems = new List<DataItem>(allDataItems);

            // Обновляем список в интерфейсе (ListBox)
            UpdateListBox();
        }

        // Метод для обновления ListBox с отфильтрованными данными
        private void UpdateListBox()
        {
            DataListBox.ItemsSource = null; // Сначала сбрасываем источник данных
            DataListBox.ItemsSource = filteredDataItems; // Устанавливаем новый источник данных (отфильтрованные элементы)
        }

        // Обработчик события для применения фильтра по ключевому слову
        private void OnApplyKeywordFilter(object sender, RoutedEventArgs e)
        {
            string keyword = KeywordTextBox.Text; // Получаем введенное пользователем ключевое слово
            if (!string.IsNullOrEmpty(keyword)) // Проверяем, что ключевое слово не пустое
            {
                // Используем делегат для фильтрации списка по ключевому слову
                filteredDataItems = allDataItems
                                    .Where(item => Filters.FilterByKeyword(item, keyword)) // Применяем фильтр
                                    .ToList(); // Преобразуем результат в список
                UpdateListBox(); // Обновляем ListBox для отображения отфильтрованных данных
            }
        }

        // Обработчик события для сброса фильтра
        private void OnResetFilter(object sender, RoutedEventArgs e)
        {
            // Восстанавливаем исходный список данных
            filteredDataItems = new List<DataItem>(allDataItems);
            UpdateListBox(); // Обновляем ListBox
        }
    }

    // Статический класс с методами фильтрации
    public static class Filters
    {
        // Метод для фильтрации по ключевому слову (делегат)
        public static bool FilterByKeyword(DataItem item, string keyword)
        {
            // Ищем ключевое слово в описании элемента данных (игнорируем регистр)
            return item.Description.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
