namespace EntityFrameworkCoreCodeFirstLab.Data
{
    public static class DataValidations
    {
        public static class Student
        {
            public const int STUDENT_NAME_MAX_LENGTH = 30;
        }

        public static class Course
        {
            public const int COURSE_NAME_MAX_LENGTH = 100;

            public const int DESCRIPTION_MAX_LENGTH = 5000;
        }

        public static class Town
        {
            public const int TOWN_NAME_MAX_LENGTH = 20;
        }
    }
}