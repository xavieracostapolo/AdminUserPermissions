import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { usePermissionRepository } from '../infraestructure'

const key: any = 'permissions'
const permissionRepository = usePermissionRepository()

export const useGetPermission = () => {
    return useQuery({ queryKey: [key], queryFn: permissionRepository.get })
}

export const useCreatePermission = () => {
    const queryClient = useQueryClient()
    return useMutation({
        mutationFn: permissionRepository.create,
        onSuccess(data, variables, context) {
            queryClient.invalidateQueries()
        },
    })
}
