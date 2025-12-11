n, k = map(int, input().split())
jokes = []

for i in range(n):
    params = list(map(int, input().split()))
    jokes.append((params, i + 1))

jokes.sort(key=lambda x: x[0], reverse=True)

result = [joke[1] for joke in jokes]
print(*result)