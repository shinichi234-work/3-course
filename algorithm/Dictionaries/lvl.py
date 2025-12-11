n = int(input())
tree = {}
all_people = set()

for _ in range(n - 1):
    child, parent = input().split()
    tree[child] = parent
    all_people.add(child)
    all_people.add(parent)

root = None
for person in all_people:
    if person not in tree:
        root = person
        break

heights = {}

def get_height(person):
    if person in heights:
        return heights[person]
    
    if person == root:
        heights[person] = 0
        return 0
    
    heights[person] = get_height(tree[person]) + 1
    return heights[person]

for person in all_people:
    get_height(person)

for person in sorted(all_people):
    print(f"{person} {heights[person]}")