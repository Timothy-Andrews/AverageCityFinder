using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AverageCityFinder
{
    public partial class MainWindow : Form
    {
        List<CityData> citiesList;

        SortedDictionary<string, List<CityData>> citiesByCountryList;
        List<CityData> citiesInSelectedCountry;


        public MainWindow()
        {
            InitializeComponent();

            LoadCityData();
            SplitCitiesByCountry();

            countryListBox.Items.Clear();
            countryListBox.Items.AddRange(citiesByCountryList.Keys.ToArray());
        }


        private void LoadCityData()
        {
            citiesList = new List<CityData>();

            string cityDataPath = Path.Combine(Directory.GetCurrentDirectory(), "worldCities.csv");

            using (var parser = new TextFieldParser(cityDataPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                // Skip the header
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    var cityData = new CityData();


                    cityData.CityName = fields[0];
                    //CityAscii = fields[1],
                    if (double.TryParse(fields[2], out double latitude))
                    {
                        cityData.Latitude = latitude;
                    }
                    if (double.TryParse(fields[3], out double longitude))
                    {
                        cityData.Longitude = longitude;
                    }
                    cityData.CountryName = fields[4];
                    //Iso2 = fields[5],
                    //Iso3 = fields[6],
                    //AdminName = fields[7],
                    //Capital = fields[8],
                    if (double.TryParse(fields[9], out double population))
                    {
                        cityData.Population = population;
                    }
                    if (long.TryParse(fields[10], out long cityID))
                    {
                        cityData.CityID = cityID;
                    }



                    citiesList.Add(cityData);
                }

            }
        }


        private void SplitCitiesByCountry()
        {
            var unorderedDictionary = new Dictionary<string, List<CityData>>();

            foreach (var city in citiesList)
            {
                if (unorderedDictionary.ContainsKey(city.CountryName))
                {
                    unorderedDictionary[city.CountryName].Add(city);
                }
                else
                {
                    unorderedDictionary.Add(city.CountryName, new List<CityData> { city });
                }
            }

            // Order each of the country city lists alphabetically
            foreach (var kvPair in unorderedDictionary)
            {
                unorderedDictionary[kvPair.Key] = kvPair.Value.OrderBy(city => city.CityName).ToList();
            }

            citiesByCountryList = new SortedDictionary<string, List<CityData>>(unorderedDictionary);
        }



        #region Country Selection

        /// <summary>
        /// This takes search text in the countrySearchBox textbox, find countries in the citiesByCountryList
        /// that match the search and populates the conutryListBox with those countries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countrySearchBox_TextChanged(object sender, EventArgs e)
        {
            countryListBox.Items.Clear();
            var searchText = countrySearchBox.Text.ToLower();

            foreach (var kvPair in citiesByCountryList)
            {
                if (kvPair.Key.ToLower().StartsWith(searchText))
                {
                    countryListBox.Items.Add(kvPair.Key);
                }
            }
        }


        /// <summary>
        /// When the user clicks on a country, we populate the cityListBox with the cities in that country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (countryListBox.SelectedIndex == -1)
            {
                return;
            }

            // Get the list of cities in the selected country
            if (!citiesByCountryList.TryGetValue(countryListBox.SelectedItem.ToString(), out citiesInSelectedCountry))
            {
                return;
            }


            cityListBox.Items.Clear();
            for (int i = 0; i < citiesInSelectedCountry.Count; i++)
            {
                cityListBox.Items.Add(citiesInSelectedCountry[i].CityName);
            }
        }

        #endregion


        #region City Selection


        private void citySearchBox_TextChanged(object sender, EventArgs e)
        {
            cityListBox.Items.Clear();
            var searchText = citySearchBox.Text.ToLower();

            foreach (var city in citiesInSelectedCountry)
            {
                if (city.CityName.ToLower().StartsWith(searchText))
                {
                    cityListBox.Items.Add(city.CityName);
                }
            }
        }

        #endregion


    }



    public class CityData
    {
        public string CityName;
        public double Latitude;
        public double Longitude;
        public string CountryName;
        public double Population;
        public long CityID;
    }
}
