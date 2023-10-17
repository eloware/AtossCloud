using System.Reflection;
using System.Text;
using AtossSoap.ATCWebService;

namespace AtossSoap;

public class Helper {
    internal static void StoreStructure(string name, webPropertyObject dataModel) {
        var markdown = new StringBuilder();
        var modelFile = new StringBuilder();

        modelFile.AppendLine($"namespace AtossSoap.Models; \r\n\r\npublic class {name}{{");

        markdown.AppendLine($"# {name}");
        markdown.AppendLine("## String properties");
        foreach (var property in dataModel.stringProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public string? {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## Integer properties");
        foreach (var property in dataModel.integerProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public int {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## Double properties");
        foreach (var property in dataModel.doubleProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public double {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## Binary properties");
        foreach (var property in dataModel.binaryProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public byte[] {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## Duration properties");
        foreach (var property in dataModel.durationProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public TimeSpan {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## List properties");
        foreach (var property in dataModel.listProperites) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            // modelFile.AppendLine($"public List<{property.type}> {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## DateTime properties");
        foreach (var property in dataModel.dateTimeProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public DateTime {property.key.ToPascalCase()} {{ get; set; }}");
        }

        markdown.AppendLine("## XML properties");
        foreach (var property in dataModel.XMLProperties) {
            markdown.AppendLine($"* {property.key.ToPascalCase()}");
            modelFile.AppendLine($"public string {property.key.ToPascalCase()} {{ get; set; }}");
        }

        modelFile.AppendLine("}");
        File.WriteAllText($"../../../../AtossSoap/Models/{name}.md", markdown.ToString());
        File.WriteAllText($"../../../../AtossSoap/Models/{name}.cs", modelFile.ToString());
    }

    internal static TModel Convert<TModel>(webPropertyObject dataModel) {
        var model = Activator.CreateInstance<TModel>();
        var properties = typeof(TModel).GetProperties();
        foreach (var property in properties) {
            var attribute = property.GetCustomAttribute<AtossNameAttribute>();

            var attributeName = attribute != null ? attribute.Name : property.Name.ToLower();

            if (property.PropertyType == typeof(string)) {
                var value = dataModel.stringProperties.FirstOrDefault(x => x.key == attributeName)?.value;
                if (value == null) continue;
                property.SetValue(model, value);
                continue;
            }

            if (property.PropertyType == typeof(int)) {
                var value = dataModel.integerProperties.FirstOrDefault(x => x.key == attributeName)?.value;
                if (value == null) continue;
                property.SetValue(model, value);
                continue;
            }

            if (property.PropertyType == typeof(double)) {
                var value = dataModel.doubleProperties.FirstOrDefault(x => x.key == attributeName)?.value;
                if (value == null) continue;
                property.SetValue(model, value);
                continue;
            }

            if (property.PropertyType == typeof(byte[])) {
                var value = dataModel.binaryProperties.FirstOrDefault(x => x.key == attributeName)?.value;
                if (value == null) continue;
                property.SetValue(model, value);
                continue;
            }

            if (property.PropertyType == typeof(TimeSpan)) {
                var value = dataModel.durationProperties.FirstOrDefault(x => x.key == attributeName)?.value;
                if (value == null) continue;
                property.SetValue(model, TimeSpan.FromHours(value ?? 0));
                continue;
            }

            if (property.PropertyType == typeof(DateTime)) {
                var value = dataModel.dateTimeProperties.FirstOrDefault(x => x.key == attributeName)?.value;
                if (value == null) continue;
                property.SetValue(model, value);
                continue;
            }
        }

        return model;
    }
}

public static class StringExtension {
    public static string ToPascalCase(this string str) => string.IsNullOrEmpty(str) ? str : char.ToUpper(str[0]) + str[1..];
}
