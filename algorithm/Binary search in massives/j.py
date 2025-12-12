n, l = map(int, input().split())

sequences = []
for _ in range(n):
    x1, d1, a, c, m = map(int, input().split())
    seq = []
    x = x1
    d = d1
    for i in range(l):
        seq.append(x)
        x += d
        d = (a * d + c) % m
    sequences.append(seq)

for i in range(n):
    seq1 = sequences[i]
    for j in range(i + 1, n):
        seq2 = sequences[j]
        
        i1 = i2 = 0
        
        for _ in range(l - 1):
            if i1 < l and (i2 >= l or seq1[i1] <= seq2[i2]):
                i1 += 1
            else:
                i2 += 1
        
        if i1 < l and (i2 >= l or seq1[i1] <= seq2[i2]):
            print(seq1[i1])
        else:
            print(seq2[i2])