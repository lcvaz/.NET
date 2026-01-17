COMANDOS UTILIZADOS
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package supabase-csharp
dotnet add package BCrypt.Net-Next

dotnet new sln --name MySolution
dotnet sln add folder1/folder2/myapp
dotnet add reference ../Application/Application.csproj 


dotnet user-secrets init
dotnet user-secrets set "Supabase:Url" "https://seu-projeto-id.supabase.co"
dotnet user-secrets set "Supabase:Key" "sua-chave-anon-aqui"
dotnet user-secrets set "Jwt:Key" "sua-chave-jwt-segura-aqui"
dotnet user-secrets set "Jwt:Issuer" "ProjetoTeste"
dotnet user-secrets set "Jwt:Audience" "ProjetoTeste"
dotnet user-secrets list