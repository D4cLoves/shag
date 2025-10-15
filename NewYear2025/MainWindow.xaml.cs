using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace NewYear2025
{
    public partial class MainWindow : Window
    {
        private const int MAX_SNOWFLAKES = 100000;
        private Random random = new Random();

        private readonly List<Ellipse> balls = new List<Ellipse>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeBallsAnimations();
            StartSnowEffect();
        }

        private void StartSnowEffect()
        {
            CompositionTarget.Rendering += OnRendering;
        }

        private void OnRendering(object sender, EventArgs e)
        {
            // Удаляем снежинки, которые достигли низа экрана
            foreach (var snowflake in SnowflakesLayer.Children.OfType<Ellipse>().ToArray())
            {
                double y = Canvas.GetTop(snowflake);
                if (y > SnowCanvas.ActualHeight)
                {
                    SnowflakesLayer.Children.Remove(snowflake);
                }
            }

            // Создаем новые снежинки
            if (SnowflakesLayer.Children.OfType<Ellipse>().Count() < MAX_SNOWFLAKES)
            {
                Ellipse snowflake = new Ellipse();
                snowflake.Fill = new SolidColorBrush(Colors.White);
                snowflake.Width = random.Next(2, 6);
                snowflake.Height = random.Next(2, 6);
                snowflake.Opacity = 0.7;

                double x = random.Next(0, (int)SnowCanvas.ActualWidth);
                double y = -snowflake.Height;

                Canvas.SetLeft(snowflake, x);
                Canvas.SetTop(snowflake, y);

                SnowflakesLayer.Children.Add(snowflake);
            }

            // Обновляем позиции существующих снежинок
            foreach (var snowflake in SnowflakesLayer.Children.OfType<Ellipse>())
            {
                double x = Canvas.GetLeft(snowflake) + random.Next(-1, 2);
                double y = Canvas.GetTop(snowflake) + random.Next(1, 3);

                Canvas.SetLeft(snowflake, x);
                Canvas.SetTop(snowflake, y);
            }
        }

        private void InitializeBallsAnimations()
        {
            balls.Add(Ball1);
            balls.Add(Ball2);
            balls.Add(Ball3);
            balls.Add(Ball4);
            balls.Add(Ball5); 
            balls.Add(Ball6); 
            balls.Add(Ball7); 
            balls.Add(Ball8); 

            foreach (var ball in balls)
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever
                };
                ball.BeginAnimation(UIElement.OpacityProperty, animation);
            }
        }
    }
}