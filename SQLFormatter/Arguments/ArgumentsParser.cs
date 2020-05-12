namespace SQLFormatter.Arguments
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ArgumentsParser
    {
        private readonly IList<string> _originalArguments;

        private readonly IDictionary<string, object> _parsedArguments = new Dictionary<string, object>();
        private readonly ISet<IArgument> _argumentsToCheck = new HashSet<IArgument>();

        public readonly IList<string> Errors = new List<string>();
        public readonly IEnumerable<string> RemainingArguments = new List<string>();

        public ArgumentsParser(string[] originalArguments)
        {
            _originalArguments = originalArguments.ToList();
        }

        public ArgumentsParser Add(IArgument argument)
        {
            _argumentsToCheck.Add(argument);
            return this;
        }

        public ArgumentsParser Parse()
        {
            foreach (var argument in _argumentsToCheck)
            {
                var error = Parse(argument);

                if (!string.IsNullOrEmpty(error))
                {
                    Errors.Add(error);
                }
            }

            return this;
        }

        public T Get<T>(string key)
        {
            if (_parsedArguments.TryGetValue(key, out object value))
            {
                return (T) value;
            }

            return default;
        }

        private string Parse(IArgument argument)
        {
            var index = GetIndexOfOptions(argument.Options);

            if (index < 0)
            {
                return $"Option {argument.Options[0]} missing";
            }

            _originalArguments.RemoveAt(index);

            if (argument.HasValue)
            {
                if (_originalArguments.Count <= index)
                {
                    return $"Missing value for option {_originalArguments[index]}";
                }

                var value = _originalArguments[index];

                var validationError = ValidateValue(argument.ValidationType, value);

                if (!string.IsNullOrEmpty(validationError))
                {
                    return validationError;
                }

                _originalArguments.RemoveAt(index);

                _parsedArguments.Add(argument.Name, value);
            }
            else
            {
                _parsedArguments.Add(argument.Name, true);
            }

            return null;
        }

        private int GetIndexOfOptions(string[] options)
        {
            foreach (var option in options)
            {
                var index = _originalArguments.IndexOf(option);

                if (index >= 0)
                {
                    return index;
                }
            }

            return -1;
        }

        private string ValidateValue(ValidationType validationType, string value)
        {
            switch (validationType)
            {
                case ValidationType.FileExists:
                    if (!File.Exists(value))
                    {
                        return $"File {value} does not exist.";
                    }
                    break;
                case ValidationType.CanCreateFile:
                    if (!CanCreateFile(value))
                    {
                        return $"Cannot create file {value}";
                    }
                    break;
            }

            return null;
        }

        private bool CanCreateFile(string filename)
        {
            bool canCreate;
            try
            {
                using (File.Create(filename)) { }
                File.Delete(filename);
                canCreate = true;
            }
            catch
            {
                canCreate = false;
            }

            return canCreate;
        }
    }
}
