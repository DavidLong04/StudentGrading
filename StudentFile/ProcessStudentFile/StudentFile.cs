using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProcessStudentFile
{
    /// <summary>
    /// Class for reading and processing a csv file of graded students in the following format: 
    /// last name, first name, grade
    /// </summary>
    public class StudentFile
    {
        /// <summary>
        /// Immutable graded student class
        /// </summary>
        public class GradedStudent
        {
            public GradedStudent(string firstName, string lastName, int score)
            {
                FirstName = firstName;
                LastName = lastName;
                Score = score;
            }

            public readonly string FirstName;
            public readonly string LastName;
            public readonly int Score;
        }

        private List<GradedStudent> _students;
        private string _filename;

        /// <summary>
        /// Get list of students
        /// </summary>
        public List<GradedStudent> Students 
        { 
            get { return _students;}
        }

        /// <summary>
        /// Constructor
        /// Reads the file into the Students list
        /// </summary>
        /// <param name="filename"></param>
        public StudentFile(string filename)
        {
            _students = ReadFile(filename);
            _filename = filename;
        }

        /// <summary>
        /// Write Students list to a file 
        /// </summary>
        /// <param name="outFilename"></param>
        public void WriteStudentsToFile(string outFilename)
        {
            if (outFilename == _filename)
                throw new ArgumentException("SortAndOuputFile: output filename cannot be the same as the input filename");

            WriteStudentsToFile(outFilename, _students);
        }

        /// <summary>
        /// Perform an in-place sort on the Students list
        /// </summary>
        /// <returns></returns>
        public List<GradedStudent> SortStudents()
        {
            return _students.OrderByDescending(x => x.Score).ThenBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }

        private List<GradedStudent> ReadFile(string filename)
        {
            var students = new List<GradedStudent>();

            using (var reader = File.OpenText(filename))
            {
                while (!reader.EndOfStream)
                {
                    var cols = reader.ReadLine().Split(',');
                    if (cols.Length != 3)
                        throw new Exception("Input file is invalid");

                    int grade;
                    if (!int.TryParse(cols[2], out grade))
                        throw new Exception("Input file is invalid");

                    students.Add(new GradedStudent(cols[1], cols[0], grade));
                }
            }

            return students;
        }

        private void WriteStudentsToFile(string filename, List<GradedStudent> students)
        {
            using (var writer = File.CreateText(filename))
            {
                foreach (var row in students)
                    writer.WriteLine(row.LastName + "," + row.FirstName + "," + row.Score.ToString());
            }
        }
    }
}
