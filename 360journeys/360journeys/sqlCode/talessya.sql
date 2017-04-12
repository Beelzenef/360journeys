delimiter ]]

drop procedure if exists seleccionarReinos]]

create procedure seleccionarReinos(cod int,
									nombreReino varchar(50),
									ciudadCapital varchar(50),
									gob varchar int)
comment ''

	declare condBusqueda varchar(255) default '';
	set @busqueda = 'select id, nombre, capital, gobernante from reino';

	-- Para b�squeda de identificador:
	if cod is not null then
		set condBusqueda = concat(' where id = ', cod);
	end if;
	-- Para b�squeda de nombre:
	if nombreReino is not null
		if condBusqueda = ''
			set condBusqueda = concat(' where nombre rlike "', nombreReino, '"');
		else
			set condBusqueda = concat(condBusqueda, ' and nombre rlike "', nombreReino, '"');
		end if;
	end if;
	-- Para b�squeda por capital:
	if ciudadCapital is not null
		if condBusqueda = ''
			set condBusqueda = concat(' where capital rlike "', ciudadCapital, '"');
		else
			set condBusqueda = concat(condBusqueda, ' and capital rlike "', ciudadCapital, '"');
		end if;
	end if;
	-- Para b�squeda por gobernante
	if gob is not null
		if condBusqueda = ''
			set condBusqueda = concat(' where gobernante = ', nombreReino);
		else
			set condBusqueda = concat(condBusqueda, ' and nombre gobernante = ', nombreReino);
		end if;
	end if;

	set @busqueda = concat(@busqueda, condBusqueda);

	prepare busquedaReino from @busqueda;
	execute busquedaReino;
	deallocate prepare busquedaReino;

end]]

delimiter ;