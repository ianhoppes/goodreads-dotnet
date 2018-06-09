using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Goodreads.Extensions;

namespace Goodreads.Models.Response
{
    /// <summary>
    /// Represents information about a book series as defined by the Goodreads API.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class Series : ApiResponse
    {
        /// <summary>
        /// The Id of the series.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The title of the series.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The description of the series.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Any notes for the series.
        /// </summary>
        public string Note { get; private set; }

        /// <summary>
        /// How many works are contained in the series total.
        /// </summary>
        public int SeriesWorksCount { get; private set; }

        /// <summary>
        /// The count of works that are considered primary in the series.
        /// </summary>
        public int PrimaryWorksCount { get; private set; }

        /// <summary>
        /// Determines if the series is usually numbered or not.
        /// </summary>
        public bool IsNumbered { get; private set; }

        /// <summary>
        /// The list of works that are in this series.
        /// </summary>
        public IReadOnlyList<SeriesWork> SeriesWorks { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "Series: Id: {0}, Title: {1}",
                    Id,
                    Title);
            }
        }

        internal override void Parse(XElement element)
        {
            Id = element.ElementAsLong("id");
            Title = element.ElementAsString("title", true);
            Description = element.ElementAsString("description", true);
            Note = element.ElementAsString("note", true);
            SeriesWorksCount = element.ElementAsInt("series_works_count");
            PrimaryWorksCount = element.ElementAsInt("primary_work_count");
            IsNumbered = element.ElementAsBool("numbered");


            var seriesWorksElement = element.Element("series_works");
            if (seriesWorksElement != null)
            {
                var seriesWorkElements = seriesWorksElement.Descendants("series_work");
                var seriesWorks = new List<SeriesWork>();
                foreach (var seriesWorkElement in seriesWorkElements)
                {
                    var seriesWork = new SeriesWork();
                    seriesWork.Parse(seriesWorkElement);
                    seriesWorks.Add(seriesWork);
                }

                SeriesWorks = seriesWorks;
            }
        }
    }
}
