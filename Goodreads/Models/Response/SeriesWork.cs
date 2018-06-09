using Goodreads.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Goodreads.Models.Response
{
    public class SeriesWork : ApiResponse
    {
        /// <summary>
        /// The Goodreads Id for this work.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// If included in a list, this defines this work's position.
        /// </summary>
        public string UserPosition { get; private set; }

        /// <summary>
        /// TODO
        /// </summary>
        public Series Series { get; private set; }

        /// <summary>
        /// TODO
        /// </summary>
        public Work Work { get; private set; }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            UserPosition = element.ElementAsString("user_position");
            var seriesElement = element.Element("series");
            if (seriesElement != null)
            {
                var series = new Series();
                series.Parse(seriesElement);
                Series = series;
            }

            var workElement = element.Element("work");
            if (workElement != null)
            {
                var work = new Work();
                work.Parse(workElement);
                Work = work;
            }
        }
    }
}
