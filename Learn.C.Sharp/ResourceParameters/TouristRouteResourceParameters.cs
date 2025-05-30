﻿using System.Text.RegularExpressions;

namespace Learn.C.Sharp.ResourceParameters
{
    public class TouristRouteResourceParameters
    {
        public string? OrderBy { get; set; }
        public string? Keyword { set; get; }
        public string RatingOperator { set; get; }
        public int? RatingValue { set; get; }

        private string _rating;
        public string Rating
        {
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    Match match = regex.Match(value);
                    if (match.Success)
                    {
                        RatingOperator = match.Groups[1].Value;
                        RatingValue = Int32.Parse(match.Groups[2].Value);
                    }
                }
                _rating = value;
            }
            get
            {
                return _rating;
            }
        }
        public string? Fields { get; set; }
    }
}
