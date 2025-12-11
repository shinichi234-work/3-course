purchases = {}

try:
    while True:
        line = input().split()
        buyer = line[0]
        item = line[1]
        quantity = int(line[2])
        
        if buyer not in purchases:
            purchases[buyer] = {}
        
        if item not in purchases[buyer]:
            purchases[buyer][item] = 0
        
        purchases[buyer][item] += quantity
except EOFError:
    pass

for buyer in sorted(purchases.keys()):
    print(f"{buyer}:")
    for item in sorted(purchases[buyer].keys()):
        print(f"{item} {purchases[buyer][item]}")