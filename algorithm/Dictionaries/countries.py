n = int(input())
city_to_country = {}

for _ in range(n):
    parts = input().split()
    country = parts[0]
    cities = parts[1:]
    
    for city in cities:
        city_to_country[city] = country

m = int(input())

for _ in range(m):
    city = input()
    print(city_to_country[city])