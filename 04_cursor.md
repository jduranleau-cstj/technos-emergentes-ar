# Curseur

1. Ajouter un `AR Raycast Manager` sur le `AR Session Origin`
2. Ajouter un `AR Plane Manager` sur le `AR Session Origin`
3. Créer un Empty avec le script [Cursor.cs](scripts/Cursor.cs).
4. À l'intérieur du empty, ajouter un GameObject enfant qui servira de visuel pour le curseur (ex: Plane avec une image)
5. Changer le scale du visuel à environ 0.05 (1 unité = 1m, donc 0.05 = 5cm
)
6. Lier le visuel et le `AR Session Origin` au script Cursor dans l'inspecteur.
