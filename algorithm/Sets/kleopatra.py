n, a, b, k = map(int, input().split())

possible = set(range(1, n + 1))
must_have_real = []

for _ in range(k):
    data = list(map(int, input().split()))
    w = data[0]
    m = data[1]
    stones = set(data[2:])
    
    weight_all_fake = m * a
    weight_with_real = (m - 1) * a + b
    
    if w == weight_all_fake:
        possible -= stones
    elif w == weight_with_real:
        must_have_real.append(stones)
    else:
        print("Impossible")
        exit()

for stones_set in must_have_real:
    possible &= stones_set

if must_have_real and len(possible) == 0:
    print("Impossible")
elif len(possible) == 0:
    print("Fail")
else:
    print(len(possible))
    print(*sorted(possible))