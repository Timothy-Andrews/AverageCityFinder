using GMap.NET;
using GMap.NET.WindowsForms;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime;
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

            SetUpCitiesLivedGridView();
            SetUpGMapWindow();
            LoadCityData();
            SplitCitiesByCountry();

            countryListBox.Items.Clear();
            countryListBox.Items.AddRange(citiesByCountryList.Keys.ToArray());

            citiesLivedList.ListChanged += CitiesLivedList_ListChanged;
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


        private void SetUpCitiesLivedGridView()
        {
            citiesLivedGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            citiesLivedGridView.AutoGenerateColumns = true;
            citiesLivedGridView.DataSource = citiesLivedList;
            citiesLivedGridView.Columns["CityName"].ReadOnly = true;
        }


        private void SetUpGMapWindow()
        {
            gmapWindow.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            // Set your initial position.
            gmapWindow.Position = new GMap.NET.PointLatLng(0, 0); // Use your desired initial latitude and longitude

            // Optional: Set zoom level, enable map dragging, and other properties.
            gmapWindow.MinZoom = 1;                                                                            // whole world zoom
            gmapWindow.MaxZoom = 20;
            gmapWindow.Zoom = 1;
            gmapWindow.ShowCenter = false;
            gmapWindow.DragButton = MouseButtons.Left; // Use the left mouse button to drag map
            gmapWindow.MouseWheelZoomEnabled = true;
            gmapWindow.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter; // Zooming will center around the mouse position
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
                cityListBox.Items.Add(citiesInSelectedCountry[i]);
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
                    cityListBox.Items.Add(city);
                }
            }
        }

        #endregion


        #region Cities Lived Management

        BindingList<CityData> citiesLivedList = new BindingList<CityData>();
        AverageCity averageCityLived = new AverageCity();


        private void addCityBtn_Click(object sender, EventArgs e)
        {
            if (cityListBox.SelectedIndex != -1)
            {
                CityData selectedCity = (CityData)cityListBox.SelectedItem;

                citiesLivedList.Add(selectedCity);
            }
        }


        private void calculateAverageLocationBtn_Click(object sender, EventArgs e)
        {
            if (citiesLivedList == null || citiesLivedList.Count == 0)
            {
                return;
            }

            double sumWeightedLat = 0;
            double sumWeightedLon = 0;
            double sumWeights = 0;

            // Normalise the time lived in each city by the longest time spent
            double maxTimeLived = citiesLivedList.Max(x => x.TimeLivedMonths);

            for (int i = 0; i < citiesLivedList.Count; i++)
            {
                double cityWeighting = (citiesLivedList[i].TimeLivedMonths / maxTimeLived);

                sumWeightedLat += citiesLivedList[i].Latitude * cityWeighting;
                sumWeightedLon += citiesLivedList[i].Longitude * cityWeighting;

                sumWeights += cityWeighting;
            }

            double averageLatitude = sumWeightedLat / sumWeights;
            double averageLongitude = sumWeightedLon / sumWeights;

            useInfoTextBox.AppendText($"Your average Latitude is {Math.Round(averageLatitude,3)}°\n");
            useInfoTextBox.AppendText($"Your average Longitude is {Math.Round(averageLongitude,3)}°\n");


            averageCityLived.Longitude = averageLongitude;
            averageCityLived.Latitude = averageLatitude;
            averageCityLived.AverageCalculated = true;

            UpdateMapCities();
        }


        private void CitiesLivedList_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateMapCities();
        }

        #endregion


        #region Map Updating

        private void UpdateMapCities()
        {
            gmapWindow.Overlays.Clear();

            var markersOverlay = new GMapOverlay("markers");
            var routesOverlay = new GMapOverlay("routes");


            for (int i = 0; i < citiesLivedList.Count; i++)
            {
                var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle( new GMap.NET.PointLatLng(citiesLivedList[i].Latitude, citiesLivedList[i].Longitude),
                                                                              GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
            
                markersOverlay.Markers.Add(marker);
            }

            if (averageCityLived.AverageCalculated)
            {
                var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(averageCityLived.Latitude, averageCityLived.Longitude),
                                                                             GMap.NET.WindowsForms.Markers.GMarkerGoogleType.purple_dot);

                markersOverlay.Markers.Add(marker);


                // Plot the lines of latitude and longitude for the average city
                List<PointLatLng> averageLatPoints = new List<PointLatLng>
                {
                    new PointLatLng(averageCityLived.Latitude, -180),
                    new PointLatLng(averageCityLived.Latitude, 180),
                };
                List<PointLatLng> averageLonPoints = new List<PointLatLng>
                {
                    new PointLatLng(-90, averageCityLived.Longitude),
                    new PointLatLng(90, averageCityLived.Longitude),
                };

                GMapRoute latRoute = new GMapRoute(averageLatPoints, "AverageLatitude")
                {
                    Stroke = new Pen(Color.Black, 2),
                };
                GMapRoute lonRoute = new GMapRoute(averageLonPoints, "AverageLatitude")
                {
                    Stroke = new Pen(Color.Black, 2),
                };


                routesOverlay.Routes.Add(latRoute);
                routesOverlay.Routes.Add(lonRoute);
            }

            gmapWindow.Overlays.Add(routesOverlay);
            gmapWindow.Overlays.Add(markersOverlay);
            gmapWindow.Invalidate();
        }

        #endregion

    }



    public class CityData
    {
        public string CityName { get; set; }
        public double Latitude;
        public double Longitude;
        public string CountryName;
        public double Population;
        public long CityID;
        public double TimeLivedMonths {get; set;}


        // This is so the listbox will display the city name on the UI
        public override string ToString()
        {
            return CityName; 
        }
    }


    public class AverageCity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool AverageCalculated { get; set; } = false;

        public CityData ClosestCity { get; set; }
        public CityData ClosestLatitudeCity { get; set; }
        public CityData ClosestLongitudeCity { get; set; }

    }
}
