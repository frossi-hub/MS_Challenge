using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakingSense.Blogging.WebAPI.Enums
{

    public enum States
    {
        /// <summary>
        /// only the author can see them, they do not appear in a blog
        /// </summary>
        Draft = 0,
        /// <summary>
        /// only the author can see them, they appear in the author's blog only if the logged in user is the author
        /// </summary>
        Private = 1,
        /// <summary>
        /// everyone can see them
        /// </summary>
        Public = 2,
        /// <summary>
        /// nobody can see them
        /// </summary>
        Delete = 3

    }
}
