using SolrNetCore.Impl;
using System;
using System.Globalization;

namespace SolrNetCore
{
    ///<summary>
    /// Defines the level of accuracy applied for distance calculations.
    /// Requires Solr 3.4+
    ///</summary>
    public enum CalculationAccuracy
    {
        ///<summary>
        /// Highest accuracy but can have a performance hit.
        ///</summary>
        Radius,

        ///<summary>
        /// Less accurany (as it draws a bounding box around the points) but faster.
        ///</summary>
        BoundingBox
    }

    ///<summary>
    /// Retrieves entries from the index based on distance from a point.
    /// Requires Solr 3.4+
    ///</summary>
    public class SolrQueryByDistance : ISelfSerializingQuery
    {
        /// <summary>
        /// Coords Solr field name
        /// </summary>
        public string FieldName { get; private set; }

        public Location Location { get; private set; }

        [Obsolete("Use the Location property instead")]
        public double PointLatitude
        {
            get
            {
                return Location.Latitude;
            }
        }

        [Obsolete("Use the Location property instead")]
        public double PointLongitude
        {
            get
            {
                return Location.Longitude;
            }
        }

        public double DistanceFromPoint { get; private set; }

        /// <summary>
        /// Calculation accuracy
        /// </summary>
        public CalculationAccuracy Accuracy { get; private set; }

        /// <summary>
        /// New query by distance using <see cref="CalculationAccuracy.Radius"/>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="pointLatitude"></param>
        /// <param name="pointLongitude"></param>
        /// <param name="distance"></param>
        [Obsolete("Use the constructor with the Location parameter")]
        public SolrQueryByDistance(string fieldName, double pointLatitude, double pointLongitude, double distance) : this(fieldName, pointLatitude, pointLongitude, distance, CalculationAccuracy.Radius) { }

        /// <summary>
        /// Query by distance using <see cref="CalculationAccuracy.Radius"/>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="location"></param>
        /// <param name="distance"></param>
        public SolrQueryByDistance(string fieldName, Location location, double distance) : this(fieldName, location, distance, CalculationAccuracy.Radius) { }

        public SolrQueryByDistance(string fieldName, Location location, double distance, CalculationAccuracy accuracy)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentNullException("fieldName");

            if (location == null)
                throw new ArgumentNullException("location");

            if (distance <= 0)
                throw new ArgumentOutOfRangeException("distance", "Distance must be greater than zero.");

            FieldName = fieldName;
            Location = location;
            DistanceFromPoint = distance;
            Accuracy = accuracy;
        }

        /// <summary>
        /// New query by distance
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="pointLatitude"></param>
        /// <param name="pointLongitude"></param>
        /// <param name="distance"></param>
        /// <param name="accuracy"></param>
        [Obsolete("Use the constructor with the Location parameter")]
        public SolrQueryByDistance(string fieldName, double pointLatitude, double pointLongitude, double distance, CalculationAccuracy accuracy) : this(fieldName, new Location(pointLatitude, pointLongitude), distance, accuracy)
        {
        }

        public string Query
        {
            get
            {
                var prefix = Accuracy == CalculationAccuracy.Radius ? "{!geofilt" : "{!bbox";
                return prefix + " pt=" + Location.ToString() + " sfield=" + FieldName + " d=" + DistanceFromPoint.ToString(CultureInfo.InvariantCulture) + "}";
            }
        }
    }
}