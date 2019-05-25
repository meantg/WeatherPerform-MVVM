using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using _1stDisplay;

namespace WeatherPerform
{
    public partial class WeatherView : Form
    {
        //ViewModel
        public interface Subject
        {
            void notifyWeather();
        }

        public interface Observer
        {
            void Update(int temperature, int humid);
        }


        //Model
        public class Weather : Subject
        {
            private int humid;
            private int temperature;
            private ArrayList observers;

            public Weather()
            {
                observers = new ArrayList();
            }

            public void registerObserver(Observer o)
            {
                observers.Add(o);
            }

            public void removerObserver(Observer o)
            {
                int i = observers.IndexOf(o);
                if (i >= 0)
                {
                    observers.Remove(i);
                }
            }
            public void notifyWeather()
            {
                foreach (Observer observer in observers)
                    observer.Update(temperature, humid);
            }

            public void NotifyChanged()
            {
                notifyWeather();
            }

            public void SetChangedWeather(int temp, int humid)
            {
                this.temperature = temp;
                this.humid = humid;

                NotifyChanged();
            }
        }

        //View
        public class CurrentConditionsDisplay : Observer
        {
            public int temperature;
            public int humid;
            private Weather weatherData;

            public CurrentConditionsDisplay(Weather weatherData)
            {
                this.weatherData = weatherData;
                weatherData.registerObserver(this);
            }

            public void Update(int temp, int humid)
            {
                this.temperature = temp;
                this.humid = humid;
            }
        }
        public WeatherView()
        {
            InitializeComponent();
            this.textBox1.Text = "HCM City";
            Weather weather = new Weather();
            CurrentConditionsDisplay curDisplay = new CurrentConditionsDisplay(weather);

            //DataBinding , Command
            weather.SetChangedWeather(36, 50);

            this.textBox3.Text = curDisplay.temperature.ToString();
            this.textBox4.Text = curDisplay.humid.ToString();

            weather.SetChangedWeather(44, 50);
            Form1 f = new Form1();
            f.textBox1.Text = this.textBox1.Text;
            f.textBox2.Text = curDisplay.temperature.ToString();
            f.textBox3.Text = curDisplay.humid.ToString();
            f.Show();                   
        }

        public static void Main()
        {
            WeatherView weatherView = new WeatherView();
            Application.EnableVisualStyles();
            Application.Run(weatherView);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
