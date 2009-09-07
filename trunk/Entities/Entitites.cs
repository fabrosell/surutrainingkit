using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Suru.TrainingKit.Entities
{
    /// <summary>
    /// Modelates an Exam.
    /// </summary>
    public class Exam
    {
        public String Name { get; set; }
        public Int16 ApprobationPercentage { get; set; }
        public Dictionary<String, Topics> ExamTopics { get; set; }        
    }

    /// <summary>
    /// Exam's topics.
    /// </summary>
    public class Topics
    {
        public Decimal TopicValueOnExam { get; set; }
        public String Name { get; set; }        
        public Dictionary<String, List<Question>> QuestionsPerLanguage { get; set; }        
    }

    /// <summary>
    /// Question with answers.
    /// </summary>
    public class Question
    {
        public Int16 Number { get; set; }
        public String Text { get; set; }
        public String Answer { get; set; }
        public String Annotation { get; set; }
        public String Language { get; set; }
    }
    
}
