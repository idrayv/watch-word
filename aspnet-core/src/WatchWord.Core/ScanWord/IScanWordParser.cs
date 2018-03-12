using System.Collections.Generic;
using System.IO;
using WatchWord.Domain.Entities;

namespace WatchWord.ScanWord
{
    /// <summary>Represents logic for parsing words in the files or streams.</summary>
    public interface IScanWordParser
    {
        /// <summary>Scans the unique words in the StreamReader of the file.</summary>
        /// <param name="material">Material entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <returns>Unsorted collection of words in file.</returns>
        List<Word> ParseUnigueWordsInFile(Material material, StreamReader stream);

        /// <summary>Scans all the words and their positions in the StreamReader of the file.</summary>
        /// <param name="material">Material entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <returns>Unsorted collection of word compositions in file.</returns>
        List<Composition> ParseAllWordsInFile(Material material, StreamReader stream);
    }
}
