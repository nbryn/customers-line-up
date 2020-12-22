import React from 'react';
import MUIList from '@material-ui/core/List';
import MUIListItem, {ListItemProps} from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';


export type ListItem = {
   id: number;
   name: string;
};

type Props = {
   listItems: ListItem[];
   onClick: (id: number) => void;
};

export const List: React.FC<Props> = ({listItems, onClick}: Props) => {
   return (
      <MUIList>
         {listItems.map((item) => (
            <MUIListItem onClick={() => onClick(item.id)}>
               <ListItemText primary={item.name} />
            </MUIListItem>
         ))}
      </MUIList>
   );
};
