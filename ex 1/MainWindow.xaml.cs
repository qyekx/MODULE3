using System;
using System.Windows;
using System.Windows.Controls;

namespace ex_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Делегат для вычисления площади
        public delegate double AreaDelegate();

        // Поле для хранения текущего делегата
        private AreaDelegate currentAreaDelegate;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateAreaButton_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    if (currentAreaDelegate != null)
                    {
                        double area = currentAreaDelegate.Invoke();
                        ResultTextBlock.Text = $"Площадь: {area:F2}";
                    }
                    else
                    {
                        ResultTextBlock.Text = "Выберите фигуру.";
                    }
                }
                catch (Exception ex)
                {
                    ResultTextBlock.Text = $"Ошибка: {ex.Message}";
                }
            }
        }
        private double CalculateCircleArea()
        {
            double radius = double.Parse(CircleRadiusTextBox.Text);
            return Math.PI * radius * radius;
        }

        // Метод для вычисления площади прямоугольника
        private double CalculateRectangleArea()
        {
            double width = double.Parse(RectangleWidthTextBox.Text);
            double height = double.Parse(RectangleHeightTextBox.Text);
            return width * height;
        }

        // Метод для вычисления площади треугольника
        private double CalculateTriangleArea()
        {
            double baseLength = double.Parse(TriangleBaseTextBox.Text);
            double height = double.Parse(TriangleHeightTextBox.Text);
            return 0.5 * baseLength * height;
        }

        private void FigureTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Скрываем все панели перед отображением нужной
            CirclePanel.Visibility = Visibility.Collapsed;
            RectanglePanel.Visibility = Visibility.Collapsed;
            TrianglePanel.Visibility = Visibility.Collapsed;

            // Определяем, какая фигура выбрана
            ComboBoxItem selectedItem = (ComboBoxItem)FigureTypeComboBox.SelectedItem;
            switch (selectedItem.Content.ToString())
            {
                case "Круг":
                    CirclePanel.Visibility = Visibility.Visible;
                    currentAreaDelegate = CalculateCircleArea;
                    break;
                case "Прямоугольник":
                    RectanglePanel.Visibility = Visibility.Visible;
                    currentAreaDelegate = CalculateRectangleArea;
                    break;
                case "Треугольник":
                    TrianglePanel.Visibility = Visibility.Visible;
                    currentAreaDelegate = CalculateTriangleArea;
                    break;
            }
        }
    }
}
