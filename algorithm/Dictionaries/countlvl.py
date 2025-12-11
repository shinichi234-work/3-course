n = int(input())
tree = {}
all_people = set()

for _ in range(n - 1):
    child, parent = input().split()
    tree[child] = parent
    all_people.add(child)
    all_people.add(parent)

def get_ancestors(person):
    ancestors = set()
    current = person
    while current in tree:
        current = tree[current]
        ancestors.add(current)
    return ancestors

results = []

try:
    while True:
        person1, person2 = input().split()
        
        ancestors1 = get_ancestors(person1)
        ancestors2 = get_ancestors(person2)
        
        if person1 in ancestors2:
            results.append(1)
        elif person2 in ancestors1:
            results.append(2)
        else:
            results.append(0)
except EOFError:
    pass

print(*results)