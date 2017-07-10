using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Service
{
    /// <summary>Represents logic for parsing words in the files or streams.</summary>
    public class ScanWordParser : IScanWordParser
    {
        public List<Word> ParseUnigueWordsInFile(Material material, StreamReader stream)
        {
            return ParseFile(material, stream, TypeResult.OnlyUniqueWordsInFile).Words;
        }

        public List<Composition> ParseAllWordsInFile(Material material, StreamReader stream)
        {
            return ParseFile(material, stream, TypeResult.CompositionOfWords).Compositions;
        }

        /// <summary>Scans the location of words in the StreamReader of the file.</summary>
        /// <param name="material">Material entity.</param>
        /// <param name="stream">Stream reader for the text file.</param>
        /// <param name="type">Type of result.</param>
        private ScanResult ParseFile(Material material, TextReader stream, TypeResult type)
        {
            var pattern = new Regex(@"[^\W_\d]([^\W_\d]|[-’`'](?=[^\W_\d]))*([^\W_\d]|['`’])?");

            var wordsLocker = new object();
            var compositionsLocker = new object();

            var fileWords = new List<Word>();
            var compositions = new List<Composition>();
            var lines = new Dictionary<int, string>();

            string currentLine;
            var counter = 1;
            while ((currentLine = stream.ReadLine()) != null)
            {
                lines.Add(counter, currentLine.ToLower());
                counter++;
            }

            Parallel.ForEach(
                lines,
                line =>
                {
                    var words = pattern.Matches(line.Value);
                    for (var i = 0; i < words.Count; i++)
                    {
                        var scanWord = GetOrCreateScanWord(wordsLocker, fileWords, material, words[i].Value);
                        if (type == TypeResult.CompositionOfWords)
                        {
                            AddWordToCompositions(compositionsLocker, compositions, scanWord, line.Key, words[i].Index + 1);
                        }
                    }
                });

            return new ScanResult { Words = fileWords, Compositions = compositions };
        }

        /// <summary>Adds word info to collection of compositions.</summary>
        /// <param name="compositionsLocker">Mutex for adding compositions.</param>
        /// <param name="compositions">The collection of word compositions.</param>
        /// <param name="scanWord">Word entity.</param>
        /// <param name="line">Serial number of the line that contains the word.</param>
        /// <param name="column">Position of the first character in word, from the beginning of the line.</param>
        private void AddWordToCompositions(
            object compositionsLocker,
            ICollection<Composition> compositions,
            Word scanWord,
            int line,
            int column)
        {
            var composition = new Composition { Word = scanWord, Line = line, Column = column };

            lock (compositionsLocker)
            {
                compositions.Add(composition);
            }
        }

        /// <summary>Gets or creates word entity using the word string.</summary>
        /// <param name="wordsLocker">Mutex for adding words.</param>
        /// <param name="fileWords">Existing words to compare.</param>
        /// <param name="material">Material containing this word.</param>
        /// <param name="wordText">The word string.</param>
        /// <returns>The <see cref="Word"/> entity.</returns>
        private Word GetOrCreateScanWord(object wordsLocker, ICollection<Word> fileWords, Material material, string wordText)
        {
            Word word;
            lock (wordsLocker)
            {
                word = fileWords.FirstOrDefault(w => w.TheWord == wordText);

                if (!Equals(word, default(Word)))
                {
                    word.Count++;
                    return word;
                }

                word = new Word { Material = material, TheWord = wordText, Count = 1 };
                fileWords.Add(word);
            }

            return word;
        }
    }
}