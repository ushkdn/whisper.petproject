[InlineData(typeof(KeyNotFoundException), 404)]
[InlineData(typeof(InvalidOperationException), 400)]
[InlineData(typeof(ArgumentNullException), 400)]
[InlineData(typeof(ArgumentException), 400)]
[InlineData(typeof(HttpRequestException), 400)]
[InlineData(typeof(Exception), 500)]