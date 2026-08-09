using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WorkBlock
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer;

        private int segundosRestantes = 30 * 60;
        private int duracionSeleccionada = 30 * 60;

        private bool estaPausado;
        private bool mantenerEncima;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += Timer_Tick;

            ActualizarTemporizador();
        }

        private void Button30_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarTiempo(30);
        }

        private void Button45_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarTiempo(45);
        }

        private void Button60_Click(object sender, RoutedEventArgs e)
        {
            SeleccionarTiempo(60);
        }

        private void SeleccionarTiempo(int minutos)
        {
            timer.Stop();

            duracionSeleccionada = minutos * 60;
            segundosRestantes = duracionSeleccionada;
            estaPausado = false;

            StartButton.Content = "▶  Iniciar";

            ActualizarTemporizador();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            estaPausado = false;

            StartButton.Content = "▶  En curso...";
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                estaPausado = true;

                StartButton.Content = "▶  Continuar";
            }
            else if (estaPausado)
            {
                timer.Start();
                estaPausado = false;

                StartButton.Content = "▶  En curso...";
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            segundosRestantes = duracionSeleccionada;
            estaPausado = false;

            StartButton.Content = "▶  Iniciar";

            ActualizarTemporizador();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (segundosRestantes > 0)
            {
                segundosRestantes--;
                ActualizarTemporizador();
                return;
            }

            timer.Stop();

            StartButton.Content = "▶  Iniciar";

            MessageBox.Show(
                "¡Bloque de trabajo terminado!\n\nEs hora de descansar 15 minutos.",
                "WorkBlock"
            );
        }

        private void ActualizarTemporizador()
        {
            int minutos = segundosRestantes / 60;
            int segundos = segundosRestantes % 60;

            TimerText.Text = $"{minutos:00}:{segundos:00}";
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            mantenerEncima = !mantenerEncima;

            Topmost = mantenerEncima;
            PinButton.Opacity = mantenerEncima ? 1 : 0.45;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}