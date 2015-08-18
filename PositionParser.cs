using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vehicle_client
{
    internal class PositionParser
    {
        private string[] RMC = new string[12];

        private DateTime time;

        // северная широта
        private string latitude="";

        // восточная долгота
        private string longutide="";

        //скорость
        private double speed=-1.0;


        //функция возвращает широту
        public string LAT()
        {
            double double_tmp;
            string str_tmp="";

            string start = latitude[0].ToString() + latitude[1].ToString();
        
            for (int i = 2; i < latitude.Count(); i++)
            {
                str_tmp += latitude[i].ToString();
            }

            double_tmp = Convert.ToDouble(start) + (Convert.ToDouble(str_tmp, CultureInfo.InvariantCulture)/60.0);

            latitude = double_tmp.ToString();
            latitude = latitude.Replace(",", ".");

            return latitude;
        }

        //функция возвращает долготу
        public string LONG()
        {
            double double_tmp;
            string str_tmp = "";

          

            string start_ = longutide[0].ToString() + longutide[1].ToString() + longutide[2].ToString();
           
        

            for (int i = 3; i < longutide.Count(); i++)
            {
                str_tmp += longutide[i];
            }

            double_tmp = Convert.ToDouble(start_) + Convert.ToDouble(str_tmp, CultureInfo.InvariantCulture) / 60;

            longutide= double_tmp.ToString();
            longutide = longutide.Replace(",", ".");

            return longutide;
        }

        //функция возвращает скорость
        public double SPEED(){return speed;}

        //функция возвращает время
        public DateTime TIME(){return time;}


        //функция парсит строку 
        public bool try_parse(string str)
        {
            try
            {

                

                //читаем строку
                //если строку подходит,разбираем ее если нет возмращаем ложь.
                if (str!=null  && str.StartsWith("$GPRMC") && str!="")
                {
                    //запишем значения строки в переменную
                    RMC = str.Split(',');

                    string str_date = RMC[9] + RMC[1];
                    string temp = str_date;
                    //если полученные данные активны
                    if (RMC[2] == "A" && RMC[1] != null && RMC[3] != null && RMC[5] != null && RMC[7] != "" &&
                        RMC[9] != null)
                    {

                        //пробуем брать время из полученной строки
                        str_date = (temp[0].ToString() + temp[1] + "-" + temp[2] + temp[3] + "-" + temp[4] + temp[5] + " "
                                 + temp[6] + temp[7] + ":" + temp[8] + temp[9] + ":" + temp[10] + temp[11] + temp[12] + temp[13] + temp[14]);

                        //конвертируем дату из строки в переменную
                        time = Convert.ToDateTime(str_date);

                        latitude = RMC[3];
                        longutide = RMC[5];


                        try
                        {

                            //double d = double.Parse(s, CultureInfo.InvariantCulture);
                            //string s = string.Format("{0:0.00}", d);

                            //конвертируем строку скорости в значение double

                            speed = Convert.ToDouble(RMC[7], CultureInfo.InvariantCulture);
                           // speed = Convert.ToDouble((string.Format("{000,00}", RMC[7])), CultureInfo.InvariantCulture);

                            //переведем узлы в километры
                           // speed *= 1.85200;

                            speed = Math.Round(speed * 1.852 , 2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString() );
                        }


                        return true;
                    }
                    else // иначе
                    {
                        //если полученная строка не активна возвратим ложь
                        if (RMC[2] == "V")
                        {
                            return false;
                        }
                        return false;
                    }

                }

                //возращаем ложь
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Обнаружен конец строки"+"!"+str+"!");
                return false;
            }


        }
    }
}

