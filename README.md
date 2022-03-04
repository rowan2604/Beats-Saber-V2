# Le-Rythme-Du-Sabre
Projet VR/ Editor de niveau du genre beat saber

## Contenu

Le jeu comporte 3 scènes : 
  - un menu principal
  - une scène de jeu
  - une scène d'édition de niveau

## Fonctionnement

Dans la scène d'édition, le joueur dispose des 2 manettes VR pour construire le niveau. 

La manette de droite est liée à la disposition des blocs et a pour fonctionnalités de :

  - Poser des blocs sur l'un des différents emplacements prévus par le GD (boutton trigger - pression)
  - Changer la couleur du bloc actuel (Grip - pression)
  - Changer la rotation du bloc actuel (Touchpad - glisser)

La manette de gauche est celle liée à la musique et au niveau, elle permet entre autres de :

  - Lancer la musique à partir de l'emplacement actuel de la grille de placement des cubes (bouton trigger - pression)
  - Stopper la musique (Grip - pression)
  - Visualiser le niveau en avancant/reculant (Touchpad - glisser)
    
Il est également possible pour le joueur de mettre en pause et d'afficher un menu en utilisant la touche menu sur n'importe laquelle des manettes.

Une fois le niveau complété, le joueur peut alors sauvegarder et lancer son niveau depuis le menu principal : le but du jeu étant de casser les blocs en rythme dans le bon sens et avec le sabre ayant la couleur correspondante.

## Améliorations à implémenter

Actuellement il n'est pas possible d'avoir plusieurs niveaux de sauvegardés, cela signifie qu'à chaque sauvegarde la précédente est écrasée.
Un menu intermédiaire permettant de choisir un slot lors de l'édit/jeu pourrait être une amélioration significative.

Il n'est pas possible de supprimer des cubes déjà positionnés, par conséquent si le joueur fait une erreur il ne peut pas la corriger.
Nous avons pensé à ajouter une option "CTRL Z" permettant au joueur d'annuler la dernière pose.

Il est très difficile de prendre en main notre interface de création de niveau, aucun indicateur n'aide le joueur à savoir :

  - où il se situe dans le niveau / dans la musique
  - le nombre de cubes positionnés
  - la couleur des sabres
  - quelles actions sont possibles

Afin d'y remédier, il faudrait intégrer une UI avec un slider pour indiquer la progression dans le niveau et designer un onglet spécifiant les actions possibles.

Enfin, notre projet était centré en premier lieu sur le bon fonctionnement d'un niveau beat saber en VR, nous n'avons donc pas un mode de jeu très complet.
Nous avons pensé à ajouter un système de score et de combo, conformément à BS étant donné que nos fonctions de détection du sens de coupe et de la couleur du sabre fonctionne.
D'autres part, des effets graphiques sur les sabres, les blocs, des animations lors de la découpes seraient un + indéniable.
