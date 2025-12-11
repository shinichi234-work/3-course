n, m = map(int, input().split())
anya = set()
for _ in range(n):
    anya.add(int(input()))

borya = set()
for _ in range(m):
    borya.add(int(input()))

common = sorted(anya & borya)
only_anya = sorted(anya - borya)
only_borya = sorted(borya - anya)

print(len(common))
print(*common)
print(len(only_anya))
print(*only_anya)
print(len(only_borya))
print(*only_borya)