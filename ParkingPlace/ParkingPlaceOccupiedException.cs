﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingPlace
{
    /// <summary>
    /// parking place is occupied by another vechile
    /// </summary>
    public class ParkingPlaceOccupiedException : Exception
    {
        const string defaultMessage = "The parkingplace is already occupied.";
        public ParkingPlaceOccupiedException() : base(defaultMessage)
        {
        }
        public ParkingPlaceOccupiedException(string message) : base(message)
        {
        }
    }
}
