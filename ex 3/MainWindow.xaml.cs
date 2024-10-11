using System; 
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls; 

namespace ex_3 
{
    // Объявление делегата TaskDelegate, который принимает строковый параметр (описание задачи)
    public delegate void TaskDelegate(string taskDescription); // Это делегат, который связывает метод с задачей

    // Класс для управления задачами
    public class TaskManager
    {
        // Список для хранения задач
        private List<Task> tasks = new List<Task>();

        // Метод для добавления задачи, принимает описание задачи и делегат для выполнения задачи
        public void AddTask(string description, TaskDelegate taskDelegate)
        {
            // Добавляем новую задачу в список с описанием и делегатом
            tasks.Add(new Task(description, taskDelegate));

            // Отображаем сообщение о добавленной задаче
            MessageBox.Show($"Задача добавлена: {description}");
        }

        // Метод для выполнения всех задач
        public void ExecuteAllTasks()
        {
            // Проходим по всем задачам в списке и выполняем их
            foreach (var task in tasks)
            {
                task.Execute(); // Вызов метода Execute для выполнения каждой задачи
            }
        }
    }

    // Класс, представляющий задачу
    public class Task
    {
        // Свойство для хранения описания задачи
        public string Description { get; }

        // Делегат для выполнения задачи — TaskDelegate, который связан с задачей
        private TaskDelegate TaskAction { get; } // Здесь используется делегат TaskDelegate

        // Конструктор для инициализации описания задачи и делегата
        public Task(string description, TaskDelegate taskAction)
        {
            Description = description; // Присваиваем описание задачи
            TaskAction = taskAction; // Присваиваем делегат, который будет выполнен
        }

        // Метод для выполнения задачи
        public void Execute()
        {
            // Если делегат не равен null, вызываем его, передавая описание задачи
            TaskAction?.Invoke(Description); // Вызов делегата для выполнения задачи
        }
    }

    // Статический класс с обработчиками задач
    public static class TaskHandlers
    {
        // Обработчик для отправки уведомлений
        public static void SendNotification(string taskDescription)
        {
            MessageBox.Show($"Отправлено уведомление: {taskDescription}"); // Показ уведомления
        }

        // Обработчик для записи задачи в журнал
        public static void LogToJournal(string taskDescription)
        {
            MessageBox.Show($"Запись в журнал: {taskDescription}"); // Показ сообщения о записи в журнал
        }

        // Обработчик для вывода задачи на консоль
        public static void PrintToConsole(string taskDescription)
        {
            Console.WriteLine($"Вывод на консоль: {taskDescription}"); // Вывод задачи на консоль
        }
    }

    /// <summary>
    /// Класс MainWindow для управления окном приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        // Экземпляр класса TaskManager для управления задачами
        private TaskManager taskManager = new TaskManager();

        // Конструктор MainWindow
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов интерфейса (определенных в XAML)
        }

        // Обработчик для кнопки "Добавить задачу"
        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            // Получаем описание задачи из текстового поля
            string taskDescription = TaskDescriptionTextBox.Text;

            // Проверяем, выбран ли тип задачи в ComboBox
            if (TaskTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                TaskDelegate taskDelegate = null; // Инициализация делегата для задачи

                // Определяем тип задачи на основе выбранного элемента в ComboBox
                switch (selectedItem.Content.ToString())
                {
                    case "Отправить уведомление":
                        taskDelegate = TaskHandlers.SendNotification; // Выбираем обработчик для уведомлений
                        break;
                    case "Запись в журнал":
                        taskDelegate = TaskHandlers.LogToJournal; // Выбираем обработчик для записи в журнал
                        break;
                    case "Вывод на консоль":
                        taskDelegate = TaskHandlers.PrintToConsole; // Выбираем обработчик для вывода на консоль
                        break;
                }

                // Если делегат выбран, добавляем задачу в TaskManager
                if (taskDelegate != null)
                {
                    taskManager.AddTask(taskDescription, taskDelegate); // Здесь используется делегат TaskDelegate
                }
                else
                {
                    // Если тип задачи не выбран, выводим сообщение
                    MessageBox.Show("Выберите тип задачи.");
                }
            }
        }

        // Обработчик для кнопки "Выполнить все задачи"
        private void OnExecuteTasks(object sender, RoutedEventArgs e)
        {
            // Выполняем все задачи, добавленные в TaskManager
            taskManager.ExecuteAllTasks();
        }
    }
}
