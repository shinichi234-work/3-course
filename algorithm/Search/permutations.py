def generate_permutations(n, current="", used=set()):
    if len(current) == n:
        print(current)
        return
    
    for i in range(1, n + 1):
        if i not in used:
            generate_permutations(n, current + str(i), used | {i})

n = int(input())
generate_permutations(n)