using System;
using System.Windows;

namespace ex_2
{
    /// <summary>
    /// Основной класс окна приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        // Поля для экземпляров классов Notification и NotificationHandler
        private Notification notification;
        private NotificationHandler handler;

        // Конструктор класса MainWindow
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна

            // Создаем новый экземпляр класса Notification, который будет отправлять уведомления
            notification = new Notification();

            // Создаем новый экземпляр класса NotificationHandler, который будет обрабатывать уведомления
            handler = new NotificationHandler();

            // Подписываем обработчик на событие уведомления сообщения
            notification.OnMessageNotification += handler.HandleMessageNotification;

            // Подписываем обработчик на событие уведомления звонка
            notification.OnCallNotification += handler.HandleCallNotification;

            // Подписываем обработчик на событие уведомления электронной почты
            notification.OnEmailNotification += handler.HandleEmailNotification;
        }

        // Обработчик для кнопки "Отправить сообщение"
        private void OnSendMessage(object sender, RoutedEventArgs e)
        {
            // Получаем текст сообщения из текстового поля
            string message = MessageTextBox.Text;

            // Отправляем сообщение через объект Notification, что вызовет событие OnMessageNotification
            notification.SendMessage(message);
        }

        // Обработчик для кнопки "Совершить звонок"
        private void OnMakeCall(object sender, RoutedEventArgs e)
        {
            // Телефонный номер для звонка (в реальной программе можно использовать текстовое поле)
            string phoneNumber = "+375333950852";

            // Совершаем звонок через объект Notification, что вызовет событие OnCallNotification
            notification.MakeCall(phoneNumber);
        }

        // Обработчик для кнопки "Отправить email"
        private void OnSendEmail(object sender, RoutedEventArgs e)
        {
            // Email для отправки (в реальной программе можно использовать текстовое поле)
            string email = "example@example.com";

            // Отправляем email через объект Notification, что вызовет событие OnEmailNotification
            notification.SendEmail(email);
        }
    }

    // Класс "Уведомление" отвечает за вызов событий уведомлений
    public class Notification
    {
        // События для различных типов уведомлений, основанные на делегате Action<string>
        // Action<string> означает, что это делегат, принимающий строку и ничего не возвращающий
        public event Action<string> OnMessageNotification;
        public event Action<string> OnCallNotification;
        public event Action<string> OnEmailNotification;

        // Метод для отправки сообщения
        public void SendMessage(string message)
        {
            // Если на событие OnMessageNotification кто-то подписан, вызываем его с переданным сообщением
            OnMessageNotification?.Invoke(message); // Вызов делегата, если он не null
        }

        // Метод для совершения звонка
        public void MakeCall(string phoneNumber)
        {
            // Если на событие OnCallNotification кто-то подписан, вызываем его с переданным номером телефона
            OnCallNotification?.Invoke(phoneNumber); // Вызов делегата, если он не null
        }

        // Метод для отправки email
        public void SendEmail(string email)
        {
            // Если на событие OnEmailNotification кто-то подписан, вызываем его с переданным адресом email
            OnEmailNotification?.Invoke(email); // Вызов делегата, если он не null
        }
    }

    // Класс "NotificationHandler" содержит методы для обработки уведомлений
    public class NotificationHandler
    {
        // Метод-обработчик для уведомления о сообщении
        public void HandleMessageNotification(string message)
        {
            // Показываем сообщение с текстом уведомления в всплывающем окне
            MessageBox.Show($"Отправлено сообщение: {message}");
        }

        // Метод-обработчик для уведомления о звонке
        public void HandleCallNotification(string phoneNumber)
        {
            // Показываем сообщение с номером телефона в всплывающем окне
            MessageBox.Show($"Совершен звонок на номер: {phoneNumber}");
        }

        // Метод-обработчик для уведомления о отправке email
        public void HandleEmailNotification(string email)
        {
            // Показываем сообщение с адресом email в всплывающем окне
            MessageBox.Show($"Отправлено электронное письмо: {email}");
        }
    }
}
