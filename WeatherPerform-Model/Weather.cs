using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WeatherPerform.Model
{
    public interface Subject    {
        void notifyWeather();
    }

    public interface Observer
    {
        void Update(int temperature, int humid);
    }

    public interface DisplayElement
    {
        void Display();
    }

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
            foreach(Observer observer in observers)
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

    public class CurrentConditionsDisplay : Observer, DisplayElement
    {
        private int temperature;
        private int humid;
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
            Display();
        }

        public void Display()
        {
        }
    }
}
