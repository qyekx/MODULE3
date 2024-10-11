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

namespace уч_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // Класс MainWindow, частичная реализация окна WPF
    {
        // Определение делегата, который принимает массив чисел и возвращает отсортированный массив
        private delegate int[] SortingMethod(int[] numbers);

        // Конструктор окна MainWindow
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна, определенных в XAML
        }

        // Обработчик события нажатия кнопки для сортировки пузырьком
        private void OnBubbleSort(object sender, RoutedEventArgs e)
        {
            PerformSort(BubbleSort); // Вызов метода PerformSort с методом BubbleSort в качестве параметра
        }

        // Обработчик события нажатия кнопки для быстрой сортировки
        private void OnQuickSort(object sender, RoutedEventArgs e)
        {
            PerformSort(QuickSort); // Вызов метода PerformSort с методом QuickSort в качестве параметра
        }

        // Метод, выполняющий сортировку в зависимости от переданного метода сортировки (делегата)
        private void PerformSort(SortingMethod sortingMethod)
        {
            try
            {
                // Получение чисел из текстового поля, разделенных запятыми, и преобразование их в массив чисел
                int[] numbers = NumbersTextBox.Text.Split(',')
                                    .Select(int.Parse)
                                    .ToArray();

                // Вызов метода сортировки, переданного через делегат
                int[] sortedNumbers = sortingMethod(numbers);

                // Вывод отсортированных чисел в текстовое поле
                SortedNumbersTextBox.Text = string.Join(", ", sortedNumbers);
            }
            catch (FormatException) // Обработка ошибки ввода, если данные некорректны
            {
                // Показ сообщения об ошибке, если пользователь ввел некорректные данные
                MessageBox.Show("Пожалуйста, введите корректные числовые данные, разделенные запятой.",
                                "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Метод для реализации сортировки пузырьком
        private int[] BubbleSort(int[] numbers)
        {
            int n = numbers.Length; // Длина массива
            int[] sortedNumbers = (int[])numbers.Clone(); // Копируем массив для сортировки
            for (int i = 0; i < n - 1; i++) // Внешний цикл
            {
                for (int j = 0; j < n - i - 1; j++) // Внутренний цикл для сравнения соседних элементов
                {
                    if (sortedNumbers[j] > sortedNumbers[j + 1]) // Если текущий элемент больше следующего
                    {
                        // Меняем местами элементы
                        int temp = sortedNumbers[j];
                        sortedNumbers[j] = sortedNumbers[j + 1];
                        sortedNumbers[j + 1] = temp;
                    }
                }
            }
            return sortedNumbers; // Возвращаем отсортированный массив
        }

        // Метод для реализации быстрой сортировки
        private int[] QuickSort(int[] numbers)
        {
            if (numbers.Length <= 1) // Базовый случай: если длина массива меньше или равна 1, возвращаем массив как есть
            {
                return numbers;
            }

            // Определение опорного элемента (pivot) - элемента в середине массива
            int pivot = numbers[numbers.Length / 2];

            // Разделение массива на два подмассива: элементы меньше опорного и элементы больше опорного
            int[] left = numbers.Where(x => x < pivot).ToArray();
            int[] right = numbers.Where(x => x > pivot).ToArray();

            // Рекурсивный вызов QuickSort для подмассивов и объединение с опорным элементом
            return QuickSort(left).Concat(new int[] { pivot }).Concat(QuickSort(right)).ToArray();
        }
    }
}
