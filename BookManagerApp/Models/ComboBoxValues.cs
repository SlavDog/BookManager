using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Models
{
    public static class ComboBoxValues
    {
        public static string[] Genres { get; set; } = [
            "Unknown", "Action and Adventure", "Biography and Memoir",
            "Children’s", "Classics", "Contemporary Fiction", "Crime and Mystery",
            "Dystopian", "Fantasy", "Graphic Novels", "Historical Fiction", "Horror",
            "Literary Fiction", "Non-Fiction", "Philosophy", "Poetry", "Romance",
            "Sci-Fi", "Self-Help", "Thriller and Suspense"
        ];

        public static string[] Bookshelves { get; set; } = [
            "Completed", "Currently Reading", "Want To Read", "Abandoned"
        ];
    }
}
