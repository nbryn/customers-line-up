import React from 'react';
import MUIList from '@mui/material/List';
import MUIListItem, {ListItemProps} from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';


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
