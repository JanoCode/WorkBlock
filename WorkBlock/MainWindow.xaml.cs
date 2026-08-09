using System;
using System.Drawing;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Forms = global::System.Windows.Forms;

namespace WorkBlock
{
    public partial class MainWindow : Window
    {
        private const int DuracionDescanso = 15 * 60;

        private readonly DispatcherTimer timer;
        private readonly Forms.NotifyIcon notifyIcon;

        private int segundosRestantes = 30 * 60;
        private int duracionSeleccionada = 30 * 60;

        private bool estaPausado;
        private bool mantenerEncima;
        private bool estaEnDescanso;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += Timer_Tick;

            notifyIcon = new Forms.NotifyIcon
            {
                Icon = SystemIcons.Application,
                Visible = true,
                Text = "WorkBlock"
            };

            ActualizarEstadoVisual();
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
            estaEnDescanso = false;

            ActualizarEstadoVisual();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            estaPausado = false;

            ActualizarTextoBotonPrincipal();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                estaPausado = true;

                ActualizarTextoBotonPrincipal();
            }
            else if (estaPausado)
            {
                timer.Start();
                estaPausado = false;

                ActualizarTextoBotonPrincipal();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            segundosRestantes = estaEnDescanso ? DuracionDescanso : duracionSeleccionada;
            estaPausado = false;

            ActualizarEstadoVisual();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (segundosRestantes <= 0)
            {
                FinalizarBloqueActual();
                return;
            }

            segundosRestantes--;
            ActualizarTemporizador();

            if (segundosRestantes == 0)
            {
                FinalizarBloqueActual();
            }
        }

        private void FinalizarBloqueActual()
        {
            timer.Stop();
            estaPausado = false;

            if (estaEnDescanso)
            {
                estaEnDescanso = false;
                segundosRestantes = duracionSeleccionada;

                ReproducirAviso();
                MostrarNotificacion(
                    "Descanso terminado",
                    "El descanso termino. Puedes comenzar un nuevo bloque.");

                ActualizarEstadoVisual();

                return;
            }

            estaEnDescanso = true;
            segundosRestantes = DuracionDescanso;

            ReproducirAviso();
            MostrarNotificacion(
                "Bloque terminado",
                "El bloque de trabajo termino. Es momento de descansar.");

            ActualizarEstadoVisual();
        }

        private void ActualizarTemporizador()
        {
            int minutos = segundosRestantes / 60;
            int segundos = segundosRestantes % 60;

            TimerText.Text = $"{minutos:00}:{segundos:00}";
        }

        private void ActualizarEstadoVisual()
        {
            ModeIndicator.Text = estaEnDescanso ? "●  DESCANSO" : "●  TRABAJO";
            SkipBreakButton.Visibility = estaEnDescanso ? Visibility.Visible : Visibility.Collapsed;

            ActualizarTemporizador();
            ActualizarTextoBotonPrincipal();
        }

        private void SkipBreakButton_Click(object sender, RoutedEventArgs e)
        {
            if (!estaEnDescanso)
            {
                return;
            }

            timer.Stop();
            estaPausado = false;
            estaEnDescanso = false;
            segundosRestantes = duracionSeleccionada;

            ActualizarEstadoVisual();
        }

        private void ActualizarTextoBotonPrincipal()
        {
            if (timer.IsEnabled)
            {
                StartButton.Content = estaEnDescanso ? "▶  Descansando..." : "▶  En curso...";
                return;
            }

            if (estaPausado)
            {
                StartButton.Content = "▶  Continuar";
                return;
            }

            StartButton.Content = estaEnDescanso ? "▶  Iniciar descanso" : "▶  Iniciar";
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

        protected override void OnClosed(EventArgs e)
        {
            notifyIcon.Dispose();
            base.OnClosed(e);
        }

        private static void ReproducirAviso()
        {
            SystemSounds.Exclamation.Play();
        }

        private void MostrarNotificacion(string titulo, string mensaje)
        {
            notifyIcon.BalloonTipTitle = titulo;
            notifyIcon.BalloonTipText = mensaje;
            notifyIcon.ShowBalloonTip(3000);
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
