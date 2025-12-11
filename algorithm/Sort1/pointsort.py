n = int(input())
points = []
for _ in range(n):
    x, y = map(int, input().split())
    distance = x**2 + y**2
    points.append((distance, x, y))

points.sort()

for distance, x, y in points:
    print(x, y)