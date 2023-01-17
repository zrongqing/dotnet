namespace EventTest
{
    /// <summary>
    /// 冷却器
    /// </summary>
    class Cooler
    {
        public Cooler(float temperature)
        {
            Temperature = temperature;
        }

        // Cooler is activated when ambient temperature is higher than this
        public float Temperature { get; set; }

        // Notifies that the temperature changed on this instance
        public void OnTemperatureChanged(float newTemperature)
        {
            if (newTemperature > Temperature)
            {
                System.Console.WriteLine($"Cooler: On, newTemperature: {newTemperature}, temperature: {Temperature}");
            }
            else
            {
                System.Console.WriteLine($"Cooler: Off,newTemperature: {newTemperature}, temperature: {Temperature}");
            }
        }
    }

    /// <summary>
    /// 加热器
    /// </summary>
    class Heater
    {
        public Heater(float temperature)
        {
            Temperature = temperature;
        }

        // Cooler is activated when ambient temperature is higher than this
        public float Temperature { get; set; }

        // Notifies that the temperature changed on this instance
        public void OnTemperatureChanged(float newTemperature)
        {
            if (newTemperature < Temperature)
            {
                System.Console.WriteLine($"Heater: On, newTemperature: {newTemperature}, temperature: {Temperature}");
            }
            else
            {
                System.Console.WriteLine($"Heater: Off, newTemperature: {newTemperature}, temperature: {Temperature}");
            }
        }

    }

    public class Thermostat
    {
        // Using C# 3.0 or later syntax.
        // Define the event publisher (initially without the sender)
        public Action<float>? OnTemperatureChange { get; set; }

        public float CurrentTemperature
        {
            get => _currentTemperature;
            set
            {
                if (value != CurrentTemperature)
                {
                    _currentTemperature = value;
                    OnTemperatureChange?.Invoke(value);
                }
            }
        }
        private float _currentTemperature;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Thermostat thermostat = new Thermostat();
            Heater heater = new Heater(100);
            Cooler cooler = new Cooler(100);

            #region 标准用法
            //thermostat.OnTemperatureChange +=
            //    heater.OnTemperatureChanged;
            //thermostat.OnTemperatureChange +=
            //    cooler.OnTemperatureChanged;
            //Console.Write("Enter temperature: ");
            //var temperature = Console.ReadLine();
            //thermostat.CurrentTemperature = int.Parse(temperature);
            #endregion

            #region 将“-=”操作符应用于委托会返回新实例

            thermostat.OnTemperatureChange +=
                heater.OnTemperatureChanged;
            thermostat.OnTemperatureChange +=
                cooler.OnTemperatureChanged;

            // 缓冲一下现在的状态
            Action<float> localOnChange = thermostat.OnTemperatureChange;
            thermostat.CurrentTemperature = int.Parse("200");

            Console.WriteLine("解除临时变量后的委托后调用");
            localOnChange -= heater.OnTemperatureChanged;
            thermostat.CurrentTemperature = int.Parse("199");

            Console.WriteLine("解除原始变量后的委托后调用");
            thermostat.OnTemperatureChange -= heater.OnTemperatureChanged;
            thermostat.CurrentTemperature = int.Parse("198");

            #endregion

        }
    }
}