import React from 'react';
import {createStyles, Theme, makeStyles} from '@material-ui/core/styles';
import MUIList from '@material-ui/core/List';
import MUIListItem, {ListItemProps} from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Divider from '@material-ui/core/Divider';
import InboxIcon from '@material-ui/icons/Inbox';
import DraftsIcon from '@material-ui/icons/Drafts';

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
