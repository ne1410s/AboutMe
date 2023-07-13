import { Component } from '@angular/core';

import { ProjectMetadataService } from '../../services/project-metadata.service';

@Component({
  selector: 'app-projects-table',
  templateUrl: './projects-table.component.html',
  styleUrls: ['./projects-table.component.scss'],
})
export class ProjectsTableComponent {

  projects$ = this.projectService.getProjects();

  constructor(private projectService: ProjectMetadataService) {}
}
